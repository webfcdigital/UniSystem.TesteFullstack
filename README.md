# UniSystem - Desafio Fullstack

Este repositório contém o código-fonte de uma aplicação fullstack construída como um desafio técnico. A aplicação consiste em um backend de API web e um frontend de aplicação de página única.

## Tecnologias Utilizadas

### Backend

*   **Framework:** ASP.NET Core
*   **Linguagem:** C#
*   **Banco de Dados:** PostgreSQL
*   **Autenticação:** JWT (JSON Web Tokens)

### Frontend

*   **Framework:** Angular
*   **Linguagem:** TypeScript
*   **Estilização:** Tailwind CSS

### Containerização

*   **Orquestração:** Docker Compose

## Começando

Para executar a aplicação localmente, você precisará ter o Docker e o Docker Compose instalados.

1.  **Clone o repositório:**

    ```bash
    git clone <url-do-repositorio>
    ```

2.  **Navegue até o diretório do projeto:**

    ```bash
    cd UniSystem.TesteFullstack
    ```

3.  **Execute a aplicação usando o Docker Compose:**

    ```bash
    docker-compose up -d
    ```

    Este comando irá construir as imagens Docker para o backend e frontend e iniciar os containers em modo detached.

4.  **Acesse as aplicações:**

    *   **Frontend:** A aplicação frontend estará disponível em `http://localhost:4200`.
    *   **Backend:** A API do backend estará disponível em `http://localhost:5001`.

## Documentação da API

A documentação da API pode ser encontrada em `http://localhost:5001/swagger`.
