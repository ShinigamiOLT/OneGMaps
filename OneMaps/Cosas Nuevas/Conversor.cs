using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;

namespace Maps
{
    class Conversor
    {
        public string decToDeg(double dec)
        {
            var str = "";
            string Este = "";
            double deg = 0, mnt = 0, sec = 0;
            if (dec < 0)
                Este = "E";
            else
                Este = "O";
            dec = Math.Abs(dec);
            deg = Math.Floor(dec);
            dec = (dec - Math.Floor(dec)) * 60;
            mnt = Math.Floor(dec);
            dec = (dec - Math.Floor(dec)) * 60;
            sec = Math.Floor(dec * 100) / 100;
            str += deg + " " + mnt + "' " + sec + "\"" + "  " + Este;
            return str;
        }
        // degree to decimal
        public double degToDec(string str)
        {
            double deg = 0;
            var regDigit = ".0123456789";
            var word = "";
            bool found;
            int pond;

            for (var n = 1; n <= 3; n++)
            {
                pond = 1;	//ponderability, divide by 60 or 3600
                found = false;
                int i;
                for (i = 0; i < str.Length; i++)
                    if (regDigit.IndexOf(str[i]) != -1)
                    {
                        found = true;
                        break;
                    }
                if (!found)
                {
                    if (str.Contains("W"))
                        deg = -deg;

                    return deg;	// no more digits?
                }

                str = str.Substring(i, str.Length - i);	// left trimming
                //find word end
                bool blanco = true;
                for (i = 0; i < str.Length; i++) if (regDigit.IndexOf(str[i]) == -1)
                    {
                        switch (str[i])
                        {
                            case '\'': pond = 60;
                                break;
                            case '\"': pond = 3600;
                                break;
                            case 'N':
                                pond = 60;
                                break;
                            case 'W':
                                pond = 60;
                                break;
                        }
                        break;
                    }
                word = str.Substring(0, i);
                str = str.Substring(i, str.Length - i);	//left trim 
                //find the degree type: deg, minute or second
                if (pond == 1)
                {
                    if (n == 2) pond = 60;
                    if (n == 3) pond = 3600;
                }
                if (word == "")
                {
                    deg = 0;
                }
                else
                    deg += Convert.ToDouble(word) / pond;
            }
            if (str.Contains("W"))
                deg = -deg;
            return deg;
        }

        private static string GetBand(double latitude)
        {
            if (latitude <= 84 && latitude >= 72)
                return "X";
            else if (latitude < 72 && latitude >= 64)
                return "W";
            else if (latitude < 64 && latitude >= 56)
                return "V";
            else if (latitude < 56 && latitude >= 48)
                return "U";
            else if (latitude < 48 && latitude >= 40)
                return "T";
            else if (latitude < 40 && latitude >= 32)
                return "S";
            else if (latitude < 32 && latitude >= 24)
                return "R";
            else if (latitude < 24 && latitude >= 16)
                return "Q";
            else if (latitude < 16 && latitude >= 8)
                return "P";
            else if (latitude < 8 && latitude >= 0)
                return "N";
            else if (latitude < 0 && latitude >= -8)
                return "M";
            else if (latitude < -8 && latitude >= -16)
                return "L";
            else if (latitude < -16 && latitude >= -24)
                return "K";
            else if (latitude < -24 && latitude >= -32)
                return "J";
            else if (latitude < -32 && latitude >= -40)
                return "H";
            else if (latitude < -40 && latitude >= -48)
                return "G";
            else if (latitude < -48 && latitude >= -56)
                return "F";
            else if (latitude < -56 && latitude >= -64)
                return "E";
            else if (latitude < -64 && latitude >= -72)
                return "D";
            else if (latitude < -72 && latitude >= -80)
                return "C";
            else
                return null;
        }

        private static int GetZone(double latitude, double longitude)
        {
            // Norway
            if (latitude >= 56 && latitude < 64 && longitude >= 3 && longitude < 13)
                return 32;

            // Spitsbergen
            if (latitude >= 72 && latitude < 84)
            {
                if (longitude >= 0 && longitude < 9)
                    return 31;
                else if (longitude >= 9 && longitude < 21)
                    return 33;
                if (longitude >= 21 && longitude < 33)
                    return 35;
                if (longitude >= 33 && longitude < 42)
                    return 37;
            }

            return (int)Math.Ceiling((longitude + 180) / 6);
        }


        //public string ConvertToUtmString(double latitude, double longitude)
        //{
        //    if (latitude < -80 || latitude > 84)
        //        return null;

        //    int zone = GetZone(latitude, longitude);
        //    string band = GetBand(latitude);

        //    //Transform to UTM
        //    CoordinateTransformationFactory ctfac = new CoordinateTransformationFactory();
        //    ICoordinateSystem wgs84geo = ProjNet.CoordinateSystems.GeographicCoordinateSystem.WGS84;
        //    ICoordinateSystem utm = ProjNet.CoordinateSystems.ProjectedCoordinateSystem.WGS84_UTM(zone, latitude > 0);
        //    ICoordinateTransformation trans = ctfac.CreateFromCoordinateSystems(wgs84geo, utm);
        //    double[] pUtm = trans.MathTransform.Transform(new double[] { longitude, latitude });

        //    double easting = pUtm[0];
        //    double northing = pUtm[1];

        //    return String.Format("{0}{1} {2:0} {3:0}", zone, band, easting, northing);
        //}

        public double[] ConvertToUtmString(double latitude, double longitude)
        {
            if (latitude < -80 || latitude > 84)
                return null;

            int zone = GetZone(latitude, longitude);
            string band = GetBand(latitude);

            //Transform to UTM
            CoordinateTransformationFactory ctfac = new CoordinateTransformationFactory();
            ICoordinateSystem wgs84geo = ProjNet.CoordinateSystems.GeographicCoordinateSystem.WGS84;
            ICoordinateSystem utm = ProjNet.CoordinateSystems.ProjectedCoordinateSystem.WGS84_UTM(zone, latitude > 0);
            ICoordinateTransformation trans = ctfac.CreateFromCoordinateSystems(wgs84geo, utm);
            return  trans.MathTransform.Transform(new double[] { longitude, latitude });
        }

        public void ToLatLon(double utmX, double utmY, string utmZone, out double latitude, out double longitude)
        {
            bool isNorthHemisphere = utmZone.Last() >= 'N';

            var diflat = -0.00066286966871111111111111111111111111;
            var diflon = -0.0003868060578;

            var zone = int.Parse(utmZone.Remove(utmZone.Length - 1));
            var c_sa = 6378137.000000;
            var c_sb = 6356752.314245;
            var e2 = Math.Pow((Math.Pow(c_sa, 2) - Math.Pow(c_sb, 2)), 0.5) / c_sb;
            var e2cuadrada = Math.Pow(e2, 2);
            var c = Math.Pow(c_sa, 2) / c_sb;
            var x = utmX - 500000;
            var y = isNorthHemisphere ? utmY : utmY - 10000000;

            var s = ((zone * 6.0) - 183.0);
            var lat = y / (c_sa * 0.9996);
            var v = (c / Math.Pow(1 + (e2cuadrada * Math.Pow(Math.Cos(lat), 2)), 0.5)) * 0.9996;
            var a = x / v;
            var a1 = Math.Sin(2 * lat);
            var a2 = a1 * Math.Pow((Math.Cos(lat)), 2);
            var j2 = lat + (a1 / 2.0);
            var j4 = ((3 * j2) + a2) / 4.0;
            var j6 = ((5 * j4) + Math.Pow(a2 * (Math.Cos(lat)), 2)) / 3.0;
            var alfa = (3.0 / 4.0) * e2cuadrada;
            var beta = (5.0 / 3.0) * Math.Pow(alfa, 2);
            var gama = (35.0 / 27.0) * Math.Pow(alfa, 3);
            var bm = 0.9996 * c * (lat - alfa * j2 + beta * j4 - gama * j6);
            var b = (y - bm) / v;
            var epsi = ((e2cuadrada * Math.Pow(a, 2)) / 2.0) * Math.Pow((Math.Cos(lat)), 2);
            var eps = a * (1 - (epsi / 3.0));
            var nab = (b * (1 - epsi)) + lat;
            var senoheps = (Math.Exp(eps) - Math.Exp(-eps)) / 2.0;
            var delt = Math.Atan(senoheps / (Math.Cos(nab)));
            var tao = Math.Atan(Math.Cos(delt) * Math.Tan(nab));

            longitude = ((delt * (180.0 / Math.PI)) + s) + diflon;
            latitude = ((lat + (1 + e2cuadrada * Math.Pow(Math.Cos(lat), 2) - (3.0 / 2.0) * e2cuadrada * Math.Sin(lat) * Math.Cos(lat) * (tao - lat)) * (tao - lat)) * (180.0 / Math.PI)) + diflat;
        }

    }
}
