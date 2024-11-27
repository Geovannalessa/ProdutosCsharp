<h3>Projeto de Intranet</h3>
Este projeto é um sistema web desenvolvido em C# com foco em se tornar uma intranet corporativa. Ele inclui funcionalidades como cadastro e gerenciamento de usuários e produtos, além de uma tela de login segura com criptografia de senhas. O projeto utiliza o Entity Framework para interação com o banco de dados SQL Server e Bootstrap para uma interface moderna e responsiva.

<h3>🚀 Funcionalidades</h3>
Implementadas
Cadastro e Gerenciamento de Usuários:

Campos: Nome, Email, Senha, CPF, CEP (com consulta automática de endereço via API), Logradouro, Bairro, Cidade, Estado.
Validação de dados no front-end e no back-end.
CRUD de Produtos:

Cadastro, edição, consulta e exclusão de produtos.
Campos: Nome e Preço.
Tela de Login:

Login seguro com criptografia de senhas.
Em Desenvolvimento
Alteração de Senha:
Atualmente, a validação de senha durante a alteração está em desenvolvimento devido ao uso de criptografia.
Aviso: A funcionalidade está sendo ajustada para garantir segurança e usabilidade.
Futuras Funcionalidades
Expansão do sistema para incluir funcionalidades específicas da intranet corporativa.
Integração com outros sistemas corporativos.
Implementação de controle de permissões para diferentes níveis de acesso.

<h3>🛠️ Tecnologias Utilizadas</h3>h3>
Linguagem: C#
Frameworks:
Entity Framework para ORM
Bootstrap para interface responsiva
Banco de Dados: SQL Server
IDE: Visual Studio

<h3>🎯 Requisitos para Rodar o Projeto</h3>
Visual Studio: Certifique-se de ter o Visual Studio instalado na sua máquina.
SQL Server: Banco de dados configurado e em execução.
.NET Framework/Core: Versão compatível com o projeto.
Conexão com o Banco de Dados: Configure a connection string no arquivo appsettings.json.

<h3>📦 Como Configurar o Projeto</h3>
Configure a Connection String:
No arquivo appsettings.json, ajuste a connection string para apontar para o seu SQL Server:
"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=SEU_BANCO;User Id=SEU_USUARIO;Password=SUA_SENHA;"
}

Crie o Banco de Dados:
No Package Manager Console ou CLI, execute o comando para aplicar as migrations:
Update-Database

Instale as Dependências:
Certifique-se de que as dependências do projeto estão instaladas. No Visual Studio, use o NuGet Package Manager para verificar ou restaurar os pacotes necessários.

Execute o Projeto:
Pressione Ctrl + F5 no Visual Studio para iniciar o projeto.

<h3>📋 Observações</h3>
Consulta de CEP: O campo CEP realiza automaticamente a consulta de endereço usando a API ViaCEP.
Criptografia de Senhas: As senhas são armazenadas no banco de dados de forma criptografada para garantir a segurança.
Alteração de Senha: Esta funcionalidade está em desenvolvimento e pode apresentar limitações.

<h3>🌟 Melhorias Futuras</h3>
Adicionar controle de permissões e níveis de acesso.
Expandir o sistema para incluir recursos administrativos personalizados.
Implementar autenticação de dois fatores (2FA).

<h3>👧💻 Autora</h3>
Desenvolvido por Geovanna Lessa.

[Infragestao.webm](https://github.com/user-attachments/assets/a9a143d8-461d-44c6-95d8-82ab1d047a6d)
