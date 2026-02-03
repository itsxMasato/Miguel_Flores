import { useEffect, useState } from "react";
import Inicio from "./Paginas/Inicio";

function App() {
  const [mensaje, setMensaje] = useState("Cargando datos...");

  useEffect(() => {
    // Aquí es donde sucede la magia de la conexión
    fetch("https://miguel-flores.onrender.com/weatherforecast")
      .then((res) => res.json())
      .then((data) => {
        console.log("Datos recibidos:", data);
        setMensaje("Conexión exitosa con el Backend");
      })
      .catch((err) => {
        console.error("Error conectando:", err);
        setMensaje("Error: No se pudo conectar al backend");
      });
  }, []);

  return (
    <div>
      <p>Estado: {mensaje}</p>
      <Inicio />
    </div>
  );
}

export default App;