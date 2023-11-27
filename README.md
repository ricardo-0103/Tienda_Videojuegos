# Tienda_Videojuegos
Apliación web para gestionar para gestionar todo el proceso básico de alquiler de una tienda de video juegos.

## Configuración inicial:
  - .NET SDK instalado
  - SQL Server instalado
## Pasos de configuración:
1. Clonar repositorio:
   ```bash
   git clone https://github.com/ricardo-0103/Tienda_Videojuegos
   cd Tienda_Videojuegos
    ```
2. Configurar la base de datos con los scripts que se encuentran en la carpeta `Scripts`
3. Actualizar la cadena de conexión de `appsetings.json` con la información de tu instancia de SQL Server:
   ```json
   "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Initial Catalog=NombreDeTuBaseDeDatos;*demás configuraciones*"
    }
   ```
## Ejecución del proyecto:
1. Navegar al directorio del proyecto y ejecutar la aplicación:
```bash
dotnet run
```
La aplicación estará disponible en `https://localhost:44390`
