document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('tarefa-form');
    const descricaoInput = document.getElementById('descricao');
    const tabela = document.getElementById('tarefas-tabela').querySelector('tbody');

    function carregarTarefas() {
        fetch('/api/tarefas')
            .then(res => res.json())
            .then(tarefas => {
                tabela.innerHTML = '';
                tarefas.forEach(tarefa => {
                    const tr = document.createElement('tr');
                    tr.innerHTML = `
                        <td>${tarefa.id}</td>
                        <td>${tarefa.descricao} ${tarefa.concluida ? '<span class="badge bg-success">Concluída</span>' : ''}</td>
                        <td>
                            <button class="btn btn-success btn-sm" ${tarefa.concluida ? 'disabled' : ''} data-concluir="${tarefa.id}">Concluir</button>
                            <button class="btn btn-danger btn-sm" data-excluir="${tarefa.id}">Excluir</button>
                        </td>
                    `;
                    tabela.appendChild(tr);
                });
            });
    }

    form.addEventListener('submit', function (e) {
        e.preventDefault();
        fetch('/api/tarefas', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ descricao: descricaoInput.value })
        }).then(() => {
            descricaoInput.value = '';
            carregarTarefas();
        });
    });

    tabela.addEventListener('click', function (e) {
        if (e.target.dataset.concluir) {
            fetch(`/api/tarefas/${e.target.dataset.concluir}/concluir`, { method: 'PUT' })
                .then(carregarTarefas);
        }
        if (e.target.dataset.excluir) {
            fetch(`/api/tarefas/${e.target.dataset.excluir}`, { method: 'DELETE' })
                .then(carregarTarefas);
        }
    });

    carregarTarefas();
});
