const API_URL = "https://miguel-flores.onrender.com/servicios";

export const getServicios = async () => {
  const res = await fetch(API_URL);
  if (!res.ok) throw new Error("Error al obtener datos");
  return res.json();
};

export const crearServicio = async (payload) => {
  const res = await fetch(API_URL, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(payload),
  });
  if (!res.ok) throw new Error("Error al crear");
  return res.json();
};

export const actualizarServicio = async (id, payload) => {
  const res = await fetch(`${API_URL}/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(payload),
  });
  if (!res.ok) throw new Error("Error al actualizar");
  return res.json();
};