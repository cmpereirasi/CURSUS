using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace ConsoleApplication4
{
    [DataContract]
    public class GeoCode
    {
        [DataMember(Name = "status")]
        public string status { get; set; }

        [DataMember(Name = "routes")]
        public Routes[] rotas { get; set; }

    }

    [DataContract]
    public class Bounds
    {
        [DataMember(Name = "northeast")]
        public Location nordeste { get; set; }

        [DataMember(Name = "southwest")]
        public Location sudoeste { get; set; }
    }

    [DataContract]
    public class Routes
    {
        [DataMember(Name = "bounds")]
        public Bounds limite { get; set; }

        [DataMember(Name = "copyright")]
        public string direitosAutorais { get; set; }

        [DataMember(Name = "legs")]
        public Legs[] trajeto { get; set; }

        [DataMember(Name = "overview_polyline")]
        public PolylineOverview point { get; set; }

        [DataMember(Name = "warnings")]
        public List<object> avisos { get; set; }

        [DataMember(Name = "waypoint_order")]
        public List<object> waypoint { get; set; }

        [DataMember(Name = "summary")]
        public string sumario { get; set; }


    }
    [DataContract]
    public class Legs
    {
        [DataMember(Name = "distance")]
        public Distance distancia { get; set; }

        [DataMember(Name = "duration")]
        public Duration duracao { get; set; }

        [DataMember(Name = "end_address")]
        public string ruaDestino { get; set; }

        [DataMember(Name = "end_location")]
        public Location destino { get; set; }

        [DataMember(Name = "start_address")]
        public string ruaOrigem { get; set; }

        [DataMember(Name = "start_location")]
        public Location origem { get; set; }

        [DataMember(Name = "steps")]
        public Steps[] caminhos { get; set; }

        [DataMember(Name = "via_waypoint")]
        public List<object> waypoint { get; set; }

    }

    [DataContract]
    public class Steps
    {
        [DataMember(Name = "distance")]
        public Distance distancia { get; set; }

        [DataMember(Name = "duration")]
        public Duration duracao { get; set; }

        [DataMember(Name = "end_location")]
        public Location destino { get; set; }

        [DataMember(Name = "html_instructions")]
        public string instrucao { get; set; }

        [DataMember(Name = "polyline")]
        public PolylineOverview point { get; set; }

        [DataMember(Name = "start_location")]
        public Location origem { get; set; }

        [DataMember(Name = "travel_mode")]
        public string veiculoUtilizado { get; set; }

    }
    [DataContract]
    public class Location
    {
      [DataMember(Name = "lat")]
        public double lat { get; set; }

        [DataMember(Name = "lng")]
        public double lng { get; set; }
    }
    [DataContract]
    public class Duration
    {
        [DataMember(Name = "text")]
        public string texto { get; set; }

        [DataMember(Name = "value")]
        public int valor { get; set; }

    }
    [DataContract]
    public class Distance
    {
        [DataMember(Name = "text")]
        public string texto { get; set; }

        [DataMember(Name = "value")]
        public int valor { get; set; }

    }
   
    [DataContract]
    public class PolylineOverview
    {
       [DataMember(Name = "points")]
        public string visaoGeral { get; set; }
    }
}
