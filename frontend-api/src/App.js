import { useEffect, useState } from "react";

function Inicio() {
  const [datos, setDatos] = useState([]);

  useEffect(() => {
    // La URL de tu backend en Render
    fetch("https://miguel-flores.onrender.com/weatherforecast")
      .then((res) => res.json())
      .then((data) => setDatos(data))
      .catch((err) => console.error("Error al conectar:", err));
  }, []);

  return (
    <div>
      <h1>Datos del Backend:</h1>
      <pre>{JSON.stringify(datos, null, 2)}</pre>
    </div>
  );
}

export default Inicio;