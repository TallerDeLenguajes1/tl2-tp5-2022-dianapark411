public class Persona{
    private int id;
    private string nombre;
    private string direccion;
    private long telefono;

    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public long Telefono { get => telefono; set => telefono = value; }

    public Persona(){

    }
    public Persona(int _id, string _nombre, string _direccion, long _telefono){
        Id = _id;
        Nombre = _nombre;
        Direccion = _direccion;
        Telefono = _telefono;
    }

    public void mostrarPersona(){
        Console.WriteLine($"Id: {Id}");
        Console.WriteLine($"Nombre: {Nombre}");
        Console.WriteLine($"Direccion: {Direccion}");
        Console.WriteLine($"Telefono: {Telefono}");
    }
}

