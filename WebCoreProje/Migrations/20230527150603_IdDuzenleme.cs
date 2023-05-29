using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCoreProje.Migrations
{
    public partial class IdDuzenleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sepet_Urunler_UrunlerUrunId",
                table: "Sepet");

            migrationBuilder.DropForeignKey(
                name: "FK_SiparisDetay_Siparis_SiparisId",
                table: "SiparisDetay");

            migrationBuilder.DropForeignKey(
                name: "FK_SiparisDetay_Urunler_UrunlerUrunId",
                table: "SiparisDetay");

            migrationBuilder.DropForeignKey(
                name: "FK_Urunler_Kategoriler_KategorilerKategoriId",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "KategoriId",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "UrunID",
                table: "SiparisDetay");

            migrationBuilder.DropColumn(
                name: "KullaniciId",
                table: "Siparis");

            migrationBuilder.DropColumn(
                name: "KullaniciId",
                table: "Sepet");

            migrationBuilder.DropColumn(
                name: "UrunId",
                table: "Sepet");

            migrationBuilder.RenameColumn(
                name: "KategorilerKategoriId",
                table: "Urunler",
                newName: "KategorilerId");

            migrationBuilder.RenameColumn(
                name: "UrunId",
                table: "Urunler",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Urunler_KategorilerKategoriId",
                table: "Urunler",
                newName: "IX_Urunler_KategorilerId");

            migrationBuilder.RenameColumn(
                name: "UrunlerUrunId",
                table: "SiparisDetay",
                newName: "UrunlerId");

            migrationBuilder.RenameColumn(
                name: "SiparisDetayId",
                table: "SiparisDetay",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_SiparisDetay_UrunlerUrunId",
                table: "SiparisDetay",
                newName: "IX_SiparisDetay_UrunlerId");

            migrationBuilder.RenameColumn(
                name: "SiparisId",
                table: "Siparis",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UrunlerUrunId",
                table: "Sepet",
                newName: "UrunlerId");

            migrationBuilder.RenameColumn(
                name: "SepetId",
                table: "Sepet",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Sepet_UrunlerUrunId",
                table: "Sepet",
                newName: "IX_Sepet_UrunlerId");

            migrationBuilder.RenameColumn(
                name: "KategoriId",
                table: "Kategoriler",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "SiparisId",
                table: "SiparisDetay",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sepet_Urunler_UrunlerId",
                table: "Sepet",
                column: "UrunlerId",
                principalTable: "Urunler",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SiparisDetay_Siparis_SiparisId",
                table: "SiparisDetay",
                column: "SiparisId",
                principalTable: "Siparis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SiparisDetay_Urunler_UrunlerId",
                table: "SiparisDetay",
                column: "UrunlerId",
                principalTable: "Urunler",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Urunler_Kategoriler_KategorilerId",
                table: "Urunler",
                column: "KategorilerId",
                principalTable: "Kategoriler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sepet_Urunler_UrunlerId",
                table: "Sepet");

            migrationBuilder.DropForeignKey(
                name: "FK_SiparisDetay_Siparis_SiparisId",
                table: "SiparisDetay");

            migrationBuilder.DropForeignKey(
                name: "FK_SiparisDetay_Urunler_UrunlerId",
                table: "SiparisDetay");

            migrationBuilder.DropForeignKey(
                name: "FK_Urunler_Kategoriler_KategorilerId",
                table: "Urunler");

            migrationBuilder.RenameColumn(
                name: "KategorilerId",
                table: "Urunler",
                newName: "KategorilerKategoriId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Urunler",
                newName: "UrunId");

            migrationBuilder.RenameIndex(
                name: "IX_Urunler_KategorilerId",
                table: "Urunler",
                newName: "IX_Urunler_KategorilerKategoriId");

            migrationBuilder.RenameColumn(
                name: "UrunlerId",
                table: "SiparisDetay",
                newName: "UrunlerUrunId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SiparisDetay",
                newName: "SiparisDetayId");

            migrationBuilder.RenameIndex(
                name: "IX_SiparisDetay_UrunlerId",
                table: "SiparisDetay",
                newName: "IX_SiparisDetay_UrunlerUrunId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Siparis",
                newName: "SiparisId");

            migrationBuilder.RenameColumn(
                name: "UrunlerId",
                table: "Sepet",
                newName: "UrunlerUrunId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Sepet",
                newName: "SepetId");

            migrationBuilder.RenameIndex(
                name: "IX_Sepet_UrunlerId",
                table: "Sepet",
                newName: "IX_Sepet_UrunlerUrunId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Kategoriler",
                newName: "KategoriId");

            migrationBuilder.AddColumn<int>(
                name: "KategoriId",
                table: "Urunler",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SiparisId",
                table: "SiparisDetay",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UrunID",
                table: "SiparisDetay",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KullaniciId",
                table: "Siparis",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KullaniciId",
                table: "Sepet",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UrunId",
                table: "Sepet",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sepet_Urunler_UrunlerUrunId",
                table: "Sepet",
                column: "UrunlerUrunId",
                principalTable: "Urunler",
                principalColumn: "UrunId");

            migrationBuilder.AddForeignKey(
                name: "FK_SiparisDetay_Siparis_SiparisId",
                table: "SiparisDetay",
                column: "SiparisId",
                principalTable: "Siparis",
                principalColumn: "SiparisId");

            migrationBuilder.AddForeignKey(
                name: "FK_SiparisDetay_Urunler_UrunlerUrunId",
                table: "SiparisDetay",
                column: "UrunlerUrunId",
                principalTable: "Urunler",
                principalColumn: "UrunId");

            migrationBuilder.AddForeignKey(
                name: "FK_Urunler_Kategoriler_KategorilerKategoriId",
                table: "Urunler",
                column: "KategorilerKategoriId",
                principalTable: "Kategoriler",
                principalColumn: "KategoriId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
