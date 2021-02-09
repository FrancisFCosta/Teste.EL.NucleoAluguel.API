using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using Teste.EL.NucleoAluguel.API.Models;

namespace Teste.EL.NucleoAluguel.API.Util
{
    public static class GeradorArquivos
    {
        public static FileStream ObterModeloContrato()
        {
            String vFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\ModeloContratoAluguel.pdf";
            System.IO.FileStream vFileStream = new System.IO.FileStream(vFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

            return vFileStream;
        }

        public static MemoryStream GerarContratoAluguel(AluguelModel aluguel)
        {
            Document document = new Document();
            MemoryStream stream = new MemoryStream();

            try
            {
                PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
                pdfWriter.CloseStream = false;

                document.Open();
                document.Add(new Paragraph("Este é o modelo de contrato de locação de veículo default."));
                document.Add(new Paragraph("Locador: ________________________"));
                document.Add(new Paragraph("Locatario: ________________________"));
                document.Add(new Paragraph("Placa: ________________________"));
                document.Add(new Paragraph("Modelo gerado com o intuito de apresentar o funcionamento da geração e retorno do pdf através da API."));
                document.Add(new Paragraph("Segue JSON com dados do Aluguel:"));
                document.Add(new Paragraph(System.Text.Json.JsonSerializer.Serialize(aluguel).ToString()));
            }
            catch (DocumentException de)
            {
                Console.Error.WriteLine(de.Message);
            }
            catch (IOException ioe)
            {
                Console.Error.WriteLine(ioe.Message);
            }

            document.Close();

            stream.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
