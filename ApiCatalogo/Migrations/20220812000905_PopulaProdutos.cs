using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalogo.Migrations
{
    public partial class PopulaProdutos : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO PRODUTO (Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId) " +
               "values ('Coca Cola Diet','Refrigerante de cola 350 ml',5.45,'coca_cola.jpg',50,getdate(),1)");

            mb.Sql("INSERT INTO PRODUTO (Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId) " +
              "values ('Lache','Atun',16.45,'coca_cola.jpg',50,getdate(),2)");

            mb.Sql("INSERT INTO PRODUTO (Nome,Descricao,Preco,ImagemUrl,Estoque,DataCadastro,CategoriaId) " +
              "values ('Pudim','doce',10.45,'pudim.jpg',50,getdate(),3)");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Produto");
        }
    }
}
