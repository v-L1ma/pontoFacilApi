<p align="center">
  <img alt="image" src="https://github.com/user-attachments/assets/0ed0aea3-ce3f-45e0-b01c-9d2ccf84981b" width="45%" />
  <img alt="image" src="https://github.com/user-attachments/assets/aeb8510e-ceae-4496-982b-3b97352a3bad" width="45%" />
  <img alt="image" src="https://github.com/user-attachments/assets/c2f40c92-d5e1-4542-85b1-3efdd15c64c0" width="45%" />
  <img alt="image" src="https://github.com/user-attachments/assets/52b9547d-b9e4-496b-9646-711c9d520103" width="45%" />
</p>

# 🕒 Ponto Fácil
<div>
  
![Angular](https://img.shields.io/badge/Angular-DD0031?style=for-the-badge&logo=angular&logoColor=white)
![TypeScript](https://img.shields.io/badge/TypeScript-3178C6?style=for-the-badge&logo=typescript&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?style=for-the-badge&logo=postgresql&logoColor=white)

</div>

---

## 📋 Sobre o Projeto

**Ponto Fácil** é um sistema de **gestão de RH** desenvolvido para facilitar o gerenciamento de **colaboradores, cargos e setores** de uma empresa.  
Seu objetivo é centralizar as informações do departamento pessoal, proporcionando um controle eficiente e moderno por meio de uma **interface intuitiva** e **API robusta**.

---

## 🧠 Funcionalidades Principais

✅ Cadastro, edição e exclusão de **colaboradores**  
✅ Controle de **cargos** e **setores**  
✅ **Autenticação com JWT**  
✅ Validação de **força de senha** e formulários reativos  
✅ Layout moderno com **Angular Material**  
✅ **Feedback visual** (Snackbar e Loading)  
✅ Arquitetura **modular e escalável**  
✅ Comunicação **Frontend ⇄ API .NET ⇄ PostgreSQL**

---

## 🖥️ Estrutura do Projeto

### **Frontend (Angular)**
src/app/  
├── core/   
│ ├── guards/ → Proteções de rotas   
│ ├── interceptors/ → Interceptadores HTTP   
│ ├── pages/ → Páginas principais (login, dashboard, etc.)  
│ └── shared/   
│ ├── components/ → Componentes reutilizáveis  
│ ├── pipes/ → Pipes personalizados  
│ └── services/ → Serviços e requisições HTTP  

Principais páginas:
- **Login** – Autenticação do usuário  
- **Dashboard** – Visão geral do sistema  
- **Gerenciar Colaboradores / Cargos / Setores** – CRUD completo  
- **Perfil e Portal** – Área de acesso do colaborador  

---

### **Backend (.NET 8)**
source/  
├── Application/   
│ ├── DTOs/ → Objetos de transferência de dados  
│ └── Services/ → Regras de negócio  
├── Domain/  
│ ├── Enums/ → Enumerações do domínio  
│ ├── Exceptions/ → Tratamento de erros  
│ └── Models/ → Entidades principais  
├── Infraestructure/  
│ ├── Data/ → Contexto do banco e configuração do EF Core  
│ └── Repositories/ → Repositórios específicos  
└── Web/  
└── Controllers/ → Endpoints da API  


Principais Controllers:
- **AuthController** – Login e autenticação JWT  
- **CargoController** – CRUD de cargos  
- **ColaboradorController** – CRUD de colaboradores  
- **SetorController** – CRUD de setores  
- **UsuarioController** – Gerenciamento de usuários  

---

## ⚙️ Tecnologias Utilizadas

### **Frontend**
- Angular 18+
- TypeScript
- SCSS (Sass)
- Angular Material
- HttpClient

### **Backend**
- .NET 8 (C#)
- Entity Framework Core
- PostgreSQL
- JWT Authentication
- ASP.NET Core Web API

---

## ⚡ Como Executar o Projeto

### **Pré-requisitos**
- Node.js (v18 ou superior)
- Angular CLI
- .NET 8 SDK
- PostgreSQL

---

## 🖥️ **1. Backend**

```bash
### Acessar a pasta do backend
cd backend

### Restaurar dependências
dotnet restore

### Aplicar as migrations
dotnet ef database update

### Rodar o servidor
dotnet run
```

A API estará disponível em:
📡 http://localhost:5083

O Swagger estará disponível em:
📡 http://localhost:5083/swagger/index.html

## 🖥️ **2. Frontend**

```bash
### Acessar a pasta do frontend
cd frontend

### Instalar dependências
npm install

### Executar a aplicação
ng serve
```
O sistema estará disponível em:
🌐 http://localhost:4200
