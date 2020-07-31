using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using Zaber.Motion;
using Zaber.Motion.Binary;

namespace ZaberMotorTest
{
    class ZaberBinaryPortControl : IDisposable
    {
        #region Properties

        /// <summary>
        /// Name of the serial port this controller is connected to.
        /// </summary>
        public string Name { get; private set; } = string.Empty;

        /// <summary>
        /// Current serial port connection.
        /// </summary>
        private Connection PortConnection { get; set; } = null;

        /// <summary>
        /// List of motors connected to the open serial port.
        /// </summary>
        private List<Device> Motors { get; set; } = new List<Device>();

        /// <summary>
        /// The number of motors connected to this port.
        /// </summary>
        public int MotorCount => Motors.Count;

        /// <summary>
        /// Method to use when moving a motor axis.
        /// </summary>
        public enum MoveMethod
        {
            Absolute,
            Relative
        }

        #endregion
        #region Constructor

        public ZaberBinaryPortControl(string PortName, bool ConnectToPort = false)
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
            PortConnection = Connection.OpenSerialPort(Name);
            Motors = PortConnection.DetectDevices().ToList();
        }

        public void Disconnect()
        {
            if (PortConnection != null && PortConnection.IsConnected)
            { PortConnection.Close(); }
        }

        /// <summary>
        /// Home all axes of the specified motor.
        /// </summary>
        /// <param name="MotorID">ID number of the motor to home.</param>
        public void Home(int MotorID)
        {
            if (MotorID < 0 || MotorID >= Motors.Count) { return; }

            _ = Motors[MotorID].Home();
        }

        /// <summary>
        /// Move the motor to the specified position.
        /// </summary>
        /// <param name="MotorID">ID number of the motor to move.</param>
        /// <param name="Position">Position to move the motor axis to.</param>
        /// <param name="Method">Move the motor to an absolute position or relative to its current position.</param>
        /// <param name="unit">Unit to convert the distance to.</param>
        /// <returns>Null if the axis was moved successfully, or the error that happened when attempting to move the axis.</returns>
        public MotionLibException MoveMotor(int MotorID, double Position, MoveMethod Method, Units unit = Units.Native)
        {
            if (MotorID < 0 || MotorID >= Motors.Count) { return new MotionLibException(Properties.Resources.MotorOutOfRange); }

            try
            {
                switch (Method)
                {
                    case MoveMethod.Absolute:
                        _ = Motors[MotorID].MoveAbsolute(Position, unit);
                        break;
                    case MoveMethod.Relative:
                        _ = Motors[MotorID].MoveRelative(Position, unit);
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
        /// Move the motor to the specified position asynchronously.
        /// </summary>
        /// <param name="MotorID">ID number of the motor to move.</param>
        /// <param name="Position">Position to move the motor axis to.</param>
        /// <param name="Method">Move the motor to an absolute position or relative to its current position.</param>
        /// <param name="unit">Unit to convert the distance to.</param>
        /// <returns>Object that contains the task for the move and any exception messages returned from the motor.</returns>
        public MotorReturn MoveMotorAsync(int MotorID, double Position, MoveMethod Method, Units unit = Units.Native)
        {
            if (MotorID < 0 || MotorID >= Motors.Count) { return new MotorReturn(new MotionLibException(Properties.Resources.MotorOutOfRange)); }

            switch (Method)
            {
                case MoveMethod.Absolute:
                    return new MotorReturn(Motors[MotorID].MoveAbsoluteAsync(Position, unit));
                case MoveMethod.Relative:
                    return new MotorReturn(Motors[MotorID].MoveRelativeAsync(Position, unit));
                default:
                    break;
            }

            return new MotorReturn(new MotionLibException(Properties.Resources.NeverReachException));
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
                    PortConnection.Dispose();
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
}
