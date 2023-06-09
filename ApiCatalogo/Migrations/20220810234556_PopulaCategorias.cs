﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalogo.Migrations
{
    public partial class PopulaCategorias : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Categoria(Nome,ImagemUrl) Values ('Bebida','bebidas.jpg')");
            mb.Sql("Insert into Categoria(Nome,ImagemUrl) Values ('Lanches','lanches.jpg')");
            mb.Sql("Insert into Categoria(Nome,ImagemUrl) Values ('Sobremesas','sobremesas.jpg')");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categorias");
        }
    }
}
