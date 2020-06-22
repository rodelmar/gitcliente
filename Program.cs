using System;
using System.Linq;
using Tiendita.Models;

namespace Tiendita
{
    class Program
    {
        static void Main(string[] args)
        {
            login();
        }

        public static void login()
        {


            Console.WriteLine("---------------------------------");
            Console.WriteLine("         Inicio sesión          ");
            Console.WriteLine("---------------------------------");

            Console.WriteLine("Ingresa usuario");
            string User = Console.ReadLine();

            Console.WriteLine("Ingresa contraseña");
            string password = Console.ReadLine();


            string password1 = Encrypt.GetSHA256(password);

            using (
                TienditaContext context = new TienditaContext())
            {


              Usuario usuario = context.Usuario.Where(a => a.User == User && a.Password == password1).FirstOrDefault();


                if (usuario != null)
                {
                    Console.WriteLine("La contraseña es correcta");
                    Menu();
                }
                else
                {
                    
                    Console.WriteLine("Datos incorrectos ");
                    login();
                }
            }
        }
        public static void Menu()
        {
            
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Menu");
            Console.WriteLine("1) Buscar producto");
            Console.WriteLine("2) Crear producto");
            Console.WriteLine("3) Actualizar producto");
            Console.WriteLine("4) Eliminar producto");
            Console.WriteLine("5) Crear Usuario ");
            Console.WriteLine("6) Buscar Usuario ");
            Console.WriteLine("7) Ventas ");

            Console.WriteLine("0) Salir");
            Console.WriteLine("---------------------------------");

            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1":
                    BuscarProductos();
                    break;
                case "2":
                    CrearProducto();
                    break;
                case "3":
                    ActualizarProducto();
                    break;
                case "4":
                    EliminarProducto();
                    break;

                case "5":
                    CrearUsuario();
                    break;

                case "6":
                    BuscarUsuario();
                    break;
                case "7":
                    Venta();
                    break;

                case "0": return;

                }

                Menu();
        
        }

        public static void BuscarProductos()
        {
           
                Console.WriteLine("Buscar prodcutos");
                Console.Write("Buscar: ");
                string buscar = Console.ReadLine();

                using (TienditaContext context = new TienditaContext())
                {
                    IQueryable<Producto> productos = context.Productos.Where(p => p.Nombre.Contains(buscar));
                    foreach (Producto producto in productos)
                    {
                        Console.WriteLine(producto);
                    }
                }
            }
            

        public static void CrearProducto()
        {
            Console.WriteLine("Crear producto");
            Producto producto = new Producto();
            producto = LlenarProducto(producto);


            using (TienditaContext context = new TienditaContext())
            {
                context.Add(producto);
                context.SaveChanges();
                Console.WriteLine("Producto creado");
            }
        }

        public static Producto LlenarProducto(Producto producto)
        {
            Console.Write("Nombre: ");
            producto.Nombre = Console.ReadLine();
            Console.Write("Descripción: ");
            producto.Descripcion = Console.ReadLine();
            Console.Write("Precio: ");
            producto.Precio = decimal.Parse(Console.ReadLine());
            Console.Write("Costo: ");
            producto.Costo = decimal.Parse(Console.ReadLine());
            Console.Write("Cantidad: ");
            producto.Cantidad = decimal.Parse(Console.ReadLine());
            Console.Write("Tamaño: ");
            producto.Tamano = Console.ReadLine();

            return producto;
        }

        public static Producto SelecionarProducto()
        {
            BuscarProductos();
            Console.Write("Seleciona el código de producto: ");
            uint id = uint.Parse(Console.ReadLine());
            using (TienditaContext context = new TienditaContext())
            {   
                Producto producto = context.Productos.Find(id);
                if(producto == null) {
                    Console.WriteLine("Producto no encontrado");
                } else
                {
                    SelecionarProducto();
                }
               return producto;
            } 
        }

        public static void ActualizarProducto()
        {
            Console.WriteLine("Actualizar producto");
            Producto producto = SelecionarProducto();
            producto = LlenarProducto(producto);
            using (TienditaContext context = new TienditaContext())
            {
                context.Update(producto);
                context.SaveChanges();
                Console.WriteLine("Producto actualizado");
            }
        }

        public static void EliminarProducto()
        {
            Console.WriteLine("Eliminar producto");
            Producto producto = SelecionarProducto();
            using (TienditaContext context = new TienditaContext())
            {
                context.Remove(producto);
                context.SaveChanges();
                Console.WriteLine("Producto eliminado");
            }
        }
        //public static void Login()
        //{
        //    Console.WriteLine("Inicio sección");

        //    Console.WriteLine("Ingresa usuario");
        //    string User = Console.ReadLine();

        //    Console.WriteLine("Ingresa contraseña");
        //    string password = Console.ReadLine();

        //    string password1 = Encrypt.GetSHA256(password);
            
        //    using (TienditaContext context = new TienditaContext())
        //    {
        //        Usuario usuario = context.Usuario.Where(a => a.User == User && a.Password == password1).FirstOrDefault();


        //        if (usuario != null)
        //        {
        //            Console.WriteLine("La contraseña es correcta");

        //        }
        //        else
        //        {
        //            Console.WriteLine("contraseña");
        //        }



        //    }
        //}



        public static void CrearUsuario()
        {
            Usuario usuario = new Usuario();

            Console.WriteLine("Crear nuevo usuario");

            Console.Write("Ingresa usuario ");
            usuario.User = Console.ReadLine();
            Console.Write("Ingresa una contraseña  ");
            usuario.Password = Console.ReadLine();

            usuario.Password = Encrypt.GetSHA256(usuario.Password);

            using (TienditaContext context = new TienditaContext())
            {
                context.Add(usuario);
                context.SaveChanges();

                Console.WriteLine("Usuario creado");
                
            }
            return;

        }
        public static void BuscarUsuario()
        {
            Console.WriteLine("Buscar usuarios");
            Console.Write("Buscar: ");
            string buscar = Console.ReadLine();

            using (TienditaContext context = new TienditaContext())
            {
                IQueryable<Usuario> usuarios = context.Usuario.Where(p => p.User.Contains(buscar));
                foreach (Usuario usuario in usuarios)
                {
                    Console.WriteLine(usuario.Id + " " + usuario.User + " " + usuario.Password);
                }
            }
        }

        public static void Venta()
        {
            Console.WriteLine("Ventas");


        }
    }
}