import { useState } from "react";
import "./ServiceForm.css";

function ServiceForm({ onGuardar, loading }) {
  const [nombre, setNombre] = useState("");
  const [descripcion, setDescripcion] = useState("");
  const [msg, setMsg] = useState("");

  const submit = async (e) => {
    e.preventDefault();
    setMsg("");

    // Validación frontend
    if (!nombre.trim() || !descripcion.trim()) {
      setMsg("Nombre y descripción son obligatorios.");
      return;
    }
    if (nombre.trim().length < 3) {
      setMsg("El nombre debe tener al menos 3 caracteres.");
      return;
    }

    // Ejecuta guardado (Home maneja el try/catch)
    const ok = await onGuardar({
      nombre: nombre.trim(),
      descripcion: descripcion.trim(),
    });

    // Si Home devuelve true, limpiamos
    if (ok) {
      setNombre("");
      setDescripcion("");
    }
  };

  return (
    <form onSubmit={submit} className="form-container">
      <h3>✨ Nuevo Servicio</h3>

      <div className="form-group">
        <input
          type="text"
          placeholder="Nombre del servicio"
          value={nombre}
          onChange={(e) => setNombre(e.target.value)}
          disabled={loading}
        />

        <input
          type="text"
          placeholder="Descripción"
          value={descripcion}
          onChange={(e) => setDescripcion(e.target.value)}
          disabled={loading}
        />

        <button type="submit" disabled={loading}>
          {loading ? "Guardando..." : "➕ Guardar"}
        </button>
      </div>

      {msg && <p className="form-message">{msg}</p>}
    </form>
  );
}

export default ServiceForm;