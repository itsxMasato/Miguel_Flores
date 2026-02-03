import "./Toast.css";

function Toast({ type = "info", text = "", onClose }) {
  if (!text) return null;

  return (
    <div className={`toast ${type}`}>
      <span><strong>{type.toUpperCase()}:</strong> {text}</span>
      <button onClick={onClose}>✕</button>
    </div>
  );
}

export default Toast;