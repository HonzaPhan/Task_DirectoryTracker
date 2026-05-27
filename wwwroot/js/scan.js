document.getElementById('scan-form').addEventListener('submit', function () {
    const input = document.querySelector('[name="Request.Path"]');
    if (!input.value.trim()) return;

    const btn = document.getElementById('scan-btn');
    const idle = document.getElementById('btn-idle');
    const loading = document.getElementById('btn-loading');

    btn.disabled = true;
    idle.classList.add('d-none');
    loading.classList.remove('d-none');
});