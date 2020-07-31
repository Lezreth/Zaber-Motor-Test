using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using Zaber.Motion;
using Zaber.Motion.Ascii;
using Zaber.Motion.Binary;

namespace ZaberMotorTest
{
    public class ZaberPortControl : IDisposable
    {
        #region Properties

        /// <summary>
        /// Name of the serial port this controller is connected to.
        /// </summary>
        public string Name { get; private set; } = string.Empty;

        /// <summary>
        /// Current ASCII serial port connection.
        /// </summary>
        private Zaber.Motion.Ascii.Connection PortConnectionAscii { get; set; } = null;

        /// <summary>
        /// Current binary serial port connection.
        /// </summary>
        private Zaber.Motion.Binary.Connection PortConnectionBinary { get; set; } = null;

        /// <summary>
        /// List of ASCII motors connected to the open serial port.
        /// </summary>
        private List<Zaber.Motion.Ascii.Device> AsciiMotors { get; set; } = new List<Zaber.Motion.Ascii.Device>();

        /// <summary>
        /// List of binary motors connected to the open serial port.
        /// </summary>
        private List<Zaber.Motion.Binary.Device> BinaryMotors { get; set; } = new List<Zaber.Motion.Binary.Device>();

        /// <summary>
        /// The number of motors connected to this port.
        /// </summary>
        public int MotorCount
        {
            get
            {
                switch (Library)
                {
                    case LibraryType.ASCII:
                        return AsciiMotors.Count;
                    case LibraryType.Binary:
                        return BinaryMotors.Count;
                    case LibraryType.NotSpecified:
                    default:
                        return 0;
                }
            }
        }

        /// <summary>
        /// Type of library this stage uses.
        /// </summary>
        public LibraryType Library { get; private set; } = LibraryType.NotSpecified;

        #endregion
        #region Enumerators

        /// <summary>
        /// Method to use when moving a motor axis.
        /// </summary>
        public enum MoveMethod
        {
            Absolute,
            Relative
        }

        /// <summary>
        /// Type of library to use for the selected stage.
        /// </summary>
        public enum LibraryType
        {
            ASCII,
            Binary,
            NotSpecified
        }

        #endregion
        #region Constructor

        public ZaberPortControl(string PortName, bool ConnectToPort = false)
        {
            Name = PortName;

            if (ConnectToPort)
            { Connect(); }
        }

        #endregion
        #region Methods

        /// <summary>
        /// Get a list of the names of available serial ports that contain Zaber motors.
        /// </summary>
        public static string[] GetSerialPorts()
        {
            return SerialPort.GetPortNames();
        }

        public void Connect()
        {
            //  Try looking for an ASCII motor
            try
            {
                PortConnectionAscii = Zaber.Motion.Ascii.Connection.OpenSerialPort(Name);
                AsciiMotors = PortConnectionAscii.DetectDevices().ToList();
                Library = LibraryType.ASCII;
            }
            catch (Exception)
            {
                //  ASCII motor not connected to specified port, try looking for a binary motor
                try
                {
                    PortConnectionBinary = Zaber.Motion.Binary.Connection.OpenSerialPort(Name);
                    BinaryMotors = PortConnectionBinary.DetectDevices().ToList();
                    Library = LibraryType.Binary;
                }
                catch (Exception)
                { throw; }
            }
        }

        public void Disconnect()
        {
            if (PortConnectionAscii != null && PortConnectionAscii.IsConnected)
            { PortConnectionAscii.Close(); }

            if (PortConnectionBinary != null && PortConnectionBinary.IsConnected)
            { PortConnectionBinary.Close(); }
        }

        /// <summary>
        /// Home the specified motor.  If the motor has more than one axes and one has not been specified, they will all be homed.
        /// </summary>
        /// <param name="MotorID">ID number of the motor to home.</param>
        /// <param name="AxisID">ID number of the axis to home.  Axis ID starts at 1.</param>
        /// <param name="WaitUntilIdle"></param>
        public void Home(int MotorID, int AxisID = -1, bool WaitUntilIdle = true)
        {
            switch (Library)
            {
                case LibraryType.ASCII:
                    if (AxisID > 0)
                    { HomeAxis(MotorID, AxisID, WaitUntilIdle); }
                    else
                    { HomeAllAxes(MotorID, WaitUntilIdle); }
                    break;
                case LibraryType.Binary:
                    _ = BinaryMotors[MotorID].Home();
                    break;
                case LibraryType.NotSpecified:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Home all axes of the specified motor.
        /// </summary>
        /// <param name="MotorID">ID number of the motor to home.</param>
        /// <param name="WaitUntilIdle">Determines whether function should return after the movement is finished or just started.</param>
        public void HomeAllAxes(int MotorID, bool WaitUntilIdle = true)
        {
            if (Library != LibraryType.ASCII) { return; }
            if (MotorID < 0 || MotorID >= AsciiMotors.Count) { return; }

            AsciiMotors[MotorID].AllAxes.Home(WaitUntilIdle);
        }

        /// <summary>
        /// Home the specified axis of the specified motor.
        /// </summary>
        /// <param name="MotorID">ID number of the motor to home.</param>
        /// <param name="AxisID">ID number of the axis to move.  Axis ID starts at 1.</param>
        /// <param name="WaitUntilIdle">Determines whether function should return after the movement is finished or just started.</param>
        public void HomeAxis(int MotorID, int AxisID, bool WaitUntilIdle = true)
        {
            if (Library != LibraryType.ASCII) { return; }
            if (MotorID < 0 || MotorID >= AsciiMotors.Count) { return; }
            if (AxisID < 1 || AxisID > AsciiMotors[MotorID].AxisCount) { return; }

            AsciiMotors[MotorID].GetAxis(AxisID).Home(WaitUntilIdle);
        }

        /// <summary>
        /// Move the motor to the specified position.  If the axis ID is not specified on an ASCII motor, the axis with ID = 1 will be moved.
        /// </summary>
        /// <param name="MotorID">ID number of the motor to move.</param>
        /// <param name="Position">Position to move the motor axis to.</param>
        /// <param name="Method">Move the motor to an absolute position or relative to its current position.</param>
        /// <param name="unit">Unit to convert the distance to.</param>
        /// <param name="WaitUntilIdle">Determines whether function should return after the movement is finished or just started.</param>
        /// <returns>Null if the axis was moved successfully, or the error that happened when attempting to move the axis.</returns>
        public MotionLibException MoveMotor(int MotorID, double Position, MoveMethod Method, Units unit = Units.Native, bool WaitUntilIdle = true)
        {
            if (Library == LibraryType.ASCII) { return MoveMotor(MotorID, 1, Position, Method, unit, WaitUntilIdle); }
            if (MotorID < 0 || MotorID >= BinaryMotors.Count) { return new MotionLibException(Properties.Resources.MotorOutOfRange); }

            try
            {
                switch (Method)
                {
                    case MoveMethod.Absolute:
                        _ = BinaryMotors[MotorID].MoveAbsolute(Position, unit);
                        break;
                    case MoveMethod.Relative:
                        _ = BinaryMotors[MotorID].MoveRelative(Position, unit);
                        break;
                    default:
                        break;
                }
            }
            catch (MotionLibException e)
            { return e; }

            return null;
        }

        /// <summary>
        /// Move the motor to the specified position.
        /// </summary>
        /// <param name="MotorID">ID number of the motor to move.</param>
        /// <param name="AxisID">ID number of the axis to move.  Axis ID starts at 1.</param>
        /// <param name="Position">Position to move the motor axis to.</param>
        /// <param name="Method">Move the motor to an absolute position or relative to its current position.</param>
        /// <param name="unit">Unit to convert the distance to.</param>
        /// <param name="WaitUntilIdle">Determines whether function should return after the movement is finished or just started.</param>
        /// <returns>Null if the axis was moved successfully, or the error that happened when attempting to move the axis.</returns>
        public MotionLibException MoveMotor(int MotorID, int AxisID, double Position, MoveMethod Method, Units unit = Units.Native, bool WaitUntilIdle = true)
        {
            if (MotorID < 0 || MotorID >= AsciiMotors.Count) { return new MotionLibException(Properties.Resources.MotorOutOfRange); }
            if (AxisID < 1 || AxisID > AsciiMotors[MotorID].AxisCount) { return new MotionLibException(Properties.Resources.AxisOutOfRange); }

            try
            {
                switch (Method)
                {
                    case MoveMethod.Absolute:
                        AsciiMotors[MotorID].GetAxis(AxisID).MoveAbsolute(Position, unit, WaitUntilIdle);
                        break;
                    case MoveMethod.Relative:
                        AsciiMotors[MotorID].GetAxis(AxisID).MoveRelative(Position, unit, WaitUntilIdle);
                        break;
                    default:
                        break;
                }
            }
            catch (MotionLibException e)
            { return e; }

            return null;
        }


        public MotorReturn MoveMotorAsync(int MotorID, double Position, MoveMethod Method, Units unit = Units.Native, bool WaitUntilIdle = true)
        {
            if (Library == LibraryType.ASCII) { MoveMotorAsync(MotorID, 1, Position, Method, unit, WaitUntilIdle); }
            if (MotorID < 0 || MotorID >= BinaryMotors.Count) { return new MotorReturn(new MotionLibException(Properties.Resources.MotorOutOfRange)); }

            switch (Method)
            {
                case MoveMethod.Absolute:
                    return new MotorReturn(BinaryMotors[MotorID].MoveAbsoluteAsync(Position, unit));
                case MoveMethod.Relative:
                    return new MotorReturn(BinaryMotors[MotorID].MoveRelativeAsync(Position, unit));
                default:
                    break;
            }

            return new MotorReturn(new MotionLibException(Properties.Resources.NeverReachException));
        }

        /// <summary>
        /// Move the motor to the specified position asynchronously.
        /// </summary>
        /// <param name="MotorID">ID number of the motor to move.</param>
        /// <param name="AxisID">ID number of the axis to move.  Axis ID starts at 1.</param>
        /// <param name="Position">Position to move the motor axis to.</param>
        /// <param name="Method">Move the motor to an absolute position or relative to its current position.</param>
        /// <param name="unit">Unit to convert the distance to.</param>
        /// <param name="WaitUntilIdle">Determines whether function should return after the movement is finished or just started.</param>
        /// <returns>Object that contains the task for the move and any exception messages returned from the motor.</returns>
        public MotorReturn MoveMotorAsync(int MotorID, int AxisID, double Position, MoveMethod Method, Units unit = Units.Native, bool WaitUntilIdle = true)
        {
            if (MotorID < 0 || MotorID >= AsciiMotors.Count) { return new MotorReturn(new MotionLibException(Properties.Resources.MotorOutOfRange)); }
            if (AxisID < 1 || AxisID > AsciiMotors[MotorID].AxisCount) { return new MotorReturn(new MotionLibException(Properties.Resources.AxisOutOfRange)); }

            switch (Method)
            {
                case MoveMethod.Absolute:
                    return new MotorReturn(AsciiMotors[MotorID].GetAxis(AxisID).MoveAbsoluteAsync(Position, unit, WaitUntilIdle));
                case MoveMethod.Relative:
                    return new MotorReturn(AsciiMotors[MotorID].GetAxis(AxisID).MoveRelativeAsync(Position, unit, WaitUntilIdle));
                default:
                    break;
            }

            return new MotorReturn(new MotionLibException(Properties.Resources.NeverReachException));
        }

        /// <summary>
        /// Returns the number of axes the selected motor has.
        /// </summary>
        /// <param name="MotorID">Motor ID to check.</param>
        /// <returns>Number of selected axes the selected motor has.</returns>
        public int AxisCount(int MotorID)
        {
            return AsciiMotors[MotorID].AxisCount;
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).

                    Disconnect();
                    PortConnectionAscii.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ZaberPortControl()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    /// <summary>
    /// Return items for the asynchronous move motor function.
    /// </summary>
    public class MotorReturn
    {
        /// <summary>
        /// Task from the async move method.
        /// </summary>
        public Task TaskAsync { get; set; } = null;

        /// <summary>
        /// Exception that occurred during a failed attempt to move the motor.
        /// </summary>
        public MotionLibException MotionException { get; set; } = null;

        /// <summary>
        /// Initialize the motor return type with the specified parameters.
        /// </summary>
        public MotorReturn() { }

        /// <summary>
        /// Initialize the motor return type with the specified parameters.
        /// </summary>
        /// <param name="AsyncTask">Task used to run the motor asynchronously.</param>
        public MotorReturn(Task AsyncTask)
        {
            TaskAsync = AsyncTask;
        }

        /// <summary>
        /// Initialize the motor return type with the specified parameters.
        /// </summary>
        /// <param name="exception">Exception that occurred during a failed attempt to move the motor.</param>
        public MotorReturn(MotionLibException exception)
        {
            MotionException = exception;
        }

        /// <summary>
        /// Initialize the motor return type with the specified parameters.
        /// </summary>
        /// <param name="AsyncTask">Task used to run the motor asynchronously.</param>
        /// <param name="exception">Exception that occurred during a failed attempt to move the motor.</param>
        public MotorReturn(Task AsyncTask, MotionLibException exception)
        {
            TaskAsync = AsyncTask;
            MotionException = exception;
        }
    }
}
