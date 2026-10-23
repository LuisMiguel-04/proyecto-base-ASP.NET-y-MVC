// Filtro de la galería (solo del lado del cliente; el resto de la lógica está en C#)
document.addEventListener("DOMContentLoaded", () => {
  const botones = document.querySelectorAll(".filtro-btn");
  const items = document.querySelectorAll(".galeria-item");
  const mensajeVacio = document.getElementById("galeria-vacio");

  if (botones.length === 0) return;

  function filtrar(categoria) {
    let visibles = 0;
    items.forEach((item) => {
      const coincide = categoria === "todos" || item.dataset.categoria === categoria;
      item.classList.toggle("oculto", !coincide);
      if (coincide) visibles++;
    });
    mensajeVacio.classList.toggle("d-none", visibles !== 0);
  }

  botones.forEach((boton) => {
    boton.addEventListener("click", () => {
      botones.forEach((b) => b.classList.remove("active"));
      boton.classList.add("active");
      filtrar(boton.dataset.categoria);
    });
  });
});
