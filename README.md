# Desafio Técnico: Sistema de Gerenciamento de Tarefas

## 📌 Objetivo
Desenvolver um sistema de gerenciamento de tarefas utilizando **C# ASP.NET MVC e Razor Pages**. O sistema deve permitir que os usuários criem, editem, visualizem e excluam tarefas, além de implementar autenticação para que cada usuário gerencie suas próprias tarefas.

## ✅ Requisitos Funcionais
- **Autenticação**: Implementar recursos de registro e login para os usuários.
- **CRUD de Tarefas**: Criar, ler, atualizar e excluir tarefas.
- **Categorias de Tarefas**: Cada tarefa pertence a uma categoria (ex: Trabalho, Pessoal, Outros) definida pelo usuário.
- **Data de Conclusão**: Cada tarefa deve ter uma data de conclusão definida pelo usuário.
- **Status da Tarefa**: Implementar um status de conclusão para as tarefas (ex: Concluída, Pendente).

## 🔧 Detalhes Técnicos
- **Models**: Criar modelos para Usuário, Tarefa e Categoria.
- **Views**: Utilizar Razor Pages para criar as interfaces de usuário, incluindo páginas de registro/login e gerenciamento de tarefas.
- **Controllers**: Desenvolver controladores para gerenciar a lógica de criação, edição, visualização e exclusão de tarefas.
- **Autenticação e Autorização**: Utilizar o **ASP.NET Core Identity** para implementar autenticação e autorização de usuários.
- **Validações**: Implementar validações apropriadas para as entradas do usuário (ex: campos obrigatórios, formatos de data, etc.).

## 🎯 Critérios de Avaliação
- **Funcionalidade**: O sistema deve atender aos requisitos funcionais descritos acima.
- **Qualidade do Código**: O código deve ser limpo, bem estruturado e comentado.
- **Usabilidade**: A interface do usuário deve ser intuitiva e fácil de usar.
- **Segurança**: Implementar boas práticas de segurança, especialmente em relação à autenticação e autorização.
- **Documentação**: Fornecer um breve documento ou README explicando como configurar e executar o projeto.

## 🚀 Tecnologias Utilizadas
- **.NET 8**
- **ASP.NET MVC**
- **Razor Pages**
- **ASP.NET Core Identity**
- **Entity Framework Core**
- **SQL Server**
- **Bootstrap** (para estilização)

## ⚡ Configuração e Execução
### 1️⃣ Pré-requisitos
- **.NET 8 SDK** instalado
- **SQL Server** configurado
- **Visual Studio** ou outro editor compatível

### 2️⃣ Clonar o repositório
```bash
git clone https://github.com/RaphaelLopes12/TaskManager.git
cd TaskManager
```

3️⃣ Configurar o banco de dados
Edite o arquivo appsettings.json e configure a connection string corretamente para o SQL Server.

### 4️⃣ Criar as Migrations
Como as migrations não foram comitadas, é necessário recriá-las:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

5️⃣ Executar a aplicação
```bash
dotnet run
```

## 🔗 Licença
Este projeto é de código aberto e pode ser utilizado livremente sob a licença MIT.
