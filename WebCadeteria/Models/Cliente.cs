public class Cliente: Persona{
    private string datosReferenciaDireccion;

    public string DatosReferenciaDireccion { get => datosReferenciaDireccion; set => datosReferenciaDireccion = value; }

    public Cliente(){}

    public Cliente(int _id, string _nombre, string _direccion, long _telefono, string _datosReferenciaDireccion):base(_id, _nombre, _direccion, _telefono){
        DatosReferenciaDireccion = _datosReferenciaDireccion;
    }
    
    public Cliente(Cliente _cli){
        Id = _cli.Id;
        Nombre = _cli.Nombre;
        Direccion = _cli.Direccion;
        Telefono = _cli.Telefono;
        DatosReferenciaDireccion = _cli.datosReferenciaDireccion;
    }

    public void mostrarCliente(){
        Console.WriteLine("INFORMACION DEL CLIENTE: ");
        this.mostrarPersona();
        Console.WriteLine($"Datos Referencia Direccion: {DatosReferenciaDireccion }");
    }
}
