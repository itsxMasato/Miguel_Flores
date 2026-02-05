// Detectar ambiente y usar URL correcta
const BASE_URL = process.

env.REACT_APP_API_URL || "http://localhost:5090/api/servicios";

async function parseResponse(res) {
  if (res.status === 204) return null; // no hay cuerpo
  const text = await res.text();
  try {
    return text ? JSON.parse(text) : null;
  } catch {
    return text;
  }
}

export async function getServicios() {
  const res = await fetch(BASE_URL);
  if (!res.ok) throw new Error("Error al cargar servicios: " + res.status);
  return parseResponse(res);
}

export async function crearServicio(payload) {
  const res = await fetch(BASE_URL, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(payload),
  });

  if (!res.ok) {
    const msg = await res.text();
    throw new Error(msg || "Error creando servicio");
  }

  return parseResponse(res);
}

export async function actualizarServicio(id, payload) {
  const res = await fetch(`${BASE_URL}/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(payload),
  });

  if (!res.ok) {
    const msg = await res.text();
    throw new Error(msg || "Error actualizando servicio");
  }

  return parseResponse(res);
}