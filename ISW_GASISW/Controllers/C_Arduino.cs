using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Importar librerias
using System.IO.Ports;
using ISW_GASISW.Models;

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

        #region Enviar Peticion
        public void EnviarPeticion(M_PeticionArduino MPA)
        {
            string Peticion = "";
            Peticion = Peticion + MPA.estacion;
            Peticion = Peticion + MPA.giro;

            this.inicializar();
            arduino.Open();
            arduino.Write(Peticion);
            arduino.Close();
        }
        #endregion
    }
}