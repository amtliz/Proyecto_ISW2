using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Importar librerias
using System.IO.Ports;

namespace ISW_GASISW.Controllers
{
    
    public class C_Arduino
    {
        //Puerto Serial Arduino-Server
        SerialPort arduino = new SerialPort();

        #region Establecer parametros de Conexion Arduino-servidor
        public void inicializar()
        {
            arduino.PortName = "COM4";
            arduino.BaudRate = 9600;
        }
        #endregion

        #region Caso prueba 1
        public void Test()
        {
            string ValorPrueba = "1R7";
            this.inicializar();
            arduino.Open();
            arduino.Write(ValorPrueba);
            arduino.Close();
        }
        #endregion
    }
}