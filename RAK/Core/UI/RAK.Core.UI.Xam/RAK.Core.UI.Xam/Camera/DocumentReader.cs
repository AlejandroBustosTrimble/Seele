using System;
using System.Threading.Tasks;

namespace RAK.Core.UI.Xam.Camera
{

    /// <summary>
    /// Representa una lectura de Codigo de Barras en DNI
    /// </summary>
    public static class DocumentReader
    {

        /// <summary>
        /// Separador
        /// </summary>
        private const char SEPARATOR = '@';

        /// <summary>
        /// Lee un codigo de DNI
        /// </summary>
        public static async Task<DocumentReaderResult> Read()
        {
            DocumentReaderResult Result = new DocumentReaderResult();
            string scannedDniString;

            try
            {
                scannedDniString = await Camera.ScanQRCode();

                if (!string.IsNullOrEmpty(scannedDniString))
                {
                    string[] scannedDniArray = scannedDniString.Split(SEPARATOR);

                    if (scannedDniArray == null || scannedDniArray.Length == 0)
                        return Result;

                    // -- Si la primera posición del array es vacia, corresponde al tipo de documento viejo
                    if (string.IsNullOrEmpty(scannedDniArray[0]))
                    {
                        Result.Data.Document = scannedDniArray[DNIPositions.Old.Number];
                        Result.Data.OrderNumber = scannedDniArray[DNIPositions.Old.OrderNumber];
                        Result.Data.Sex = scannedDniArray[DNIPositions.Old.Sex];
                        Result.Data.BirthDay = scannedDniArray[DNIPositions.Old.BirthDay];
                        Result.Data.Name= scannedDniArray[DNIPositions.Old.Name];
                        Result.Data.Nationality = scannedDniArray[DNIPositions.Old.Nationality];
                        Result.Data.SurName = scannedDniArray[DNIPositions.Old.SurName];
                    }
                    else
                    {
                        Result.Data.Document = scannedDniArray[DNIPositions.New.Number];
                        Result.Data.OrderNumber = scannedDniArray[DNIPositions.New.OrderNumber];
                        Result.Data.Name = scannedDniArray[DNIPositions.New.Names];
                        Result.Data.SurName = scannedDniArray[DNIPositions.New.Surname];
                        Result.Data.Sex = scannedDniArray[DNIPositions.New.Sex];
                        Result.Data.BirthDay = scannedDniArray[DNIPositions.New.BirthDay];
                    }

                    Result.ReadOK = true;
                    return Result;
                }
                else
                {
                    return Result;
                }
            }
            catch (Exception ex)
            {
                Result.ReadOK = false;
                return Result;
            }
        }

        /// <summary>
        /// Indica segun la respuesta (Actualmente, renaper da 'M' para Hombre y F para Mujer).
        /// </summary>
        public static bool IsMale(string Sex)
        {
            if (Sex == "M")
                return true;
            else return false;
        }

    }

    public class DocumentReaderResult
    {
        public bool ReadOK { get; set; }
        public DocumentData Data { get; set; } = new DocumentData();
    }

    public class DocumentData
    {
        /// <summary>
        /// DNI
        /// </summary>
        public string Document { get; set; }

        /// <summary>
        /// Sexo
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// Nro de Tramite
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// Nombres
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Apellido
        /// </summary>
        public string SurName { get; set; }
        /// <summary>
        /// Fecha nacimiento
        /// </summary>
        public string BirthDay { get; set; }
        /// <summary>
        /// Nacionalidad
        /// </summary>
        public string Nationality { get; set; }
    }

    /// <summary>
    /// Accesor a valores para comparar
    /// </summary>
    internal static class DNIPositions
    {
        public static class Old
        {
            public static int Sex { get { return (int)OldDNIPositions.Sex; } }
            public static int Number { get { return (int)OldDNIPositions.Number; } }
            public static int OrderNumber { get { return (int)OldDNIPositions.OrderNumber; } }
            public static int Name { get { return (int)OldDNIPositions.Names; } }
            public static int SurName { get { return (int)OldDNIPositions.Surname; } }
            public static int BirthDay { get { return (int)OldDNIPositions.BirthDay; } }
            public static int Nationality { get { return (int)OldDNIPositions.Nationality; } }
        }

        public static class New
        {
            public static int Sex { get { return (int)NewDNIPositions.Sex; } }
            public static int Number { get { return (int)NewDNIPositions.Number; } }
            public static int OrderNumber { get { return (int)NewDNIPositions.OrderNumber; } }
            public static int Names { get { return (int)NewDNIPositions.Names; } }
            public static int Surname { get { return (int)NewDNIPositions.Surname; } }
            public static int BirthDay { get { return (int)NewDNIPositions.BirthDay; } }

        }
    }

    //@14589408    @A@1@AMATILLI @WALTER LEONARDO @ARGENTINA@01 / 02 / 1962@M@21 / 04 / 2011@00046735975@7000 @21 / 04 / 2026@411@0@ILR: 2.01 C: 110223.01(No Cap.)@UNIDAD #04  || S/N: 0040>2008>>0003
    internal enum OldDNIPositions
    {
        Nationality = 6,
        BirthDay = 7,
        OrderNumber = 10,
        Surname = 4,
        Names = 5,
        Sex = 8,
        Number = 1
    }

    // 00405070649@AGUSTI @GASTON CRISTIAN @M@34812212@B@08 / 11 / 1989@21 / 10 / 2015@207
    internal enum NewDNIPositions
    {
        OrderNumber = 0,
        Surname = 1,
        Names = 2,
        Sex = 3,
        Number = 4,
        BirthDay = 6
    }

}