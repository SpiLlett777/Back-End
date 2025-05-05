document.addEventListener('DOMContentLoaded', () => {
    console.log('app.js загружен и работает');
    const btn = document.getElementById('btn');
    if (btn) {
        btn.addEventListener('click', () => alert('Кнопка нажата!'));
    }
});