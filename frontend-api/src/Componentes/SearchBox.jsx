import "./SearchBox.css";

function SearchBox({ value, onChange }) {
  return (
    <div className="search-container">
      <input
        type="text"
        placeholder="🔍 Buscar servicio..."
        value={value}
        onChange={e => onChange(e.target.value)}
      />
    </div>
  );
}
export default SearchBox;