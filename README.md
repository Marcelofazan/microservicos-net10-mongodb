## 🚀🛠️ Microservicos-Net10-Mongodb
Exemplo de criação de WebAPI de arquitetura distribuída, desenvolvida em .NET 10 com banco de dados MongoDB

#### O que você vai encontrar neste projeto

| Tecnologia | Descrição |
|-----------|-----------|
| **IRepository<T>**| Implementação de um repositório genérico focado no MongoDB, será criado um banco de dados para cada projeto WebAPI |
| **Nuget**  | Criação de pacote NuGet para estratégia (DRY), para evitar repetição de código no compartilhamento entre microserviços. |

#### Requisitos do Projeto
- Necessário servidor MongoDB local instalado **(localhost:27017)**

Necessário acomplamento de serviços, o Inventário depende da execução de Catalogo.

- Executar WebAPI Catalogo para cadastro de produtos: 
-Builder da WebAPI Catalogo **(https://localhost:7026/api/items/)** ou **(https://localhost:7026/swagger/index.html)**

Após Clicar com botão inverso no projeto Inventário e selecionar Definir projeto de de Inicialiação. 

- Executar WebAPI Inventário, ela dependendo de localhost de WebAPI de Catalogo.  
Builder da WebAPI Inventario **(https://localhost:7012/api/Items)** ou **(https://localhost:7012/swagger/index.html)**

#### 🧪 Executar Endpoints
- Enviar POST / Catalogo para criação do banco Entity: **https://localhost:7026/api/items/**, selecionar Guia Body e enviar RAW e enviar o seguinte Content-Type: application/json

```json
{
	"Name" : "Abacaxi",
	"Description" : "Abacaxi",
	"Price" : 6.00
}
```

- Enviar POST / Inventário para criação do banco Entity: **https://localhost:7012/api/Items**, selecionar Guia Body e enviar RAW e enviar o seguinte Content-Type: application/json
```json
{
    "UserId" : "a4ce3b13-acef-2cd2-8855-89cfaaac5224",
    "CatalogItemId" : "fe373bcd-5b51-427d-adf1-6a0771f06c16",
    "Quantity" : 80
}
```

📦 O Projeto Pacote foi compilado e hospedado em **(nuget.org)** para ser usado como **(using Pacote;)** 
- [Criar e publicar um pacote NuGet usando o Visual Studio](https://learn.microsoft.com/pt-br/nuget/quickstart/create-and-publish-a-package-using-visual-studio?tabs=netcore-cli)
