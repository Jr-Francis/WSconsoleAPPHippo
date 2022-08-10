using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;

internal class WSconsoleAPPHippo
{
    static void Main(string[] args)
    {
        string linkTxt = @"C:\Users\suele\Desktop\Maria Maria\VStudio22\HippoLinks.txt";
        string priceTxt = @"C:\Users\suele\Desktop\Maria Maria\VStudio22\Hippoprecos.txt";
        //string linkTxt = @"C:\Users\Crawler\Desktop\Maria Maria\VStudio22\HippoLinks.txt";
        //string priceTxt = @"C:\Users\Crawler\Desktop\Maria Maria\VStudio22\Hippoprecos.txt";
        var listaLinks = new List<string> { };
        var listaPrices = new List<string> { };
        string dadosListaLink;

        if (File.Exists(@"C:\Users\suele\Desktop\Maria Maria\VStudio22\HippoLinks.txt"))
        //if (File.Exists(@"C:\Users\Crawler\Desktop\Maria Maria\VStudio22\HippoLinks.txt"))
        {
            using (StreamReader sr = new StreamReader(linkTxt)) //se arquivo n existir voltar p o menu
            {
                Console.WriteLine("Lista de usuários atual:");
                while ((dadosListaLink = sr.ReadLine()) != null)
                {
                    listaLinks.Add(dadosListaLink);
                    Console.WriteLine(dadosListaLink);
                }
                sr.Close();
            }
            var totalItens = listaLinks.Count;
            Console.WriteLine("");
            Console.WriteLine("Total de itens:");
            Console.WriteLine(totalItens);
            Console.WriteLine("");

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");

            IWebDriver driver = new ChromeDriver(chromeOptions);
            var counter = 0;

            foreach (var product in listaLinks)
            {
                //Thread.Sleep(500);
                driver.Navigate().GoToUrl(product);

                Thread.Sleep(2000);
                var productOn = driver.FindElements(By.XPath("/html/body/app-root/app-produto-detalhe/div/div/div[1]/div/div/app-tag-preco/div/div[2]"));
                var productPromo2 = driver.FindElements(By.XPath("/html/body/app-root/app-produto-detalhe/div/div/div[1]/div/div/app-tag-preco/div[2]/div[2]"));
                if(productPromo2.Count > 0)
                {
                    var pricePromo2 = driver.FindElement(By.XPath("/html/body/app-root/app-produto-detalhe/div/div/div[1]/div/div/app-tag-preco/div[2]/div[2]"));
                    //var i = pricePromo.Count;
                    listaPrices.Add(pricePromo2.Text);
                    counter++;
                    Console.WriteLine($"{counter} de {totalItens}");
                    Console.WriteLine(pricePromo2.Text);
                    Debug.Print(pricePromo2.Text);
                }
                else if (productOn.Count > 0)
                {
                    // var pricePromo = driver.FindElement(By.XPath("/html/head/meta[13]"));
                    var pricePromo = driver.FindElement(By.XPath("/html/body/app-root/app-produto-detalhe/div/div/div[1]/div/div/app-tag-preco/div/div[2]"));
                    //var i = pricePromo.Count;
                    listaPrices.Add(pricePromo.Text);
                    counter++;
                    Console.WriteLine($"{counter} de {totalItens}");
                    Console.WriteLine(pricePromo.Text);
                    Debug.Print(pricePromo.Text);
                }
                else
                {
                    Debug.Print("R$ 0,00");
                    listaPrices.Add("R$ 0,00");
                    counter++;
                    Console.WriteLine($"{counter} de {totalItens}");
                    Console.WriteLine("R$ 0,00");
                }
                
            }

            using (StreamWriter sw = new StreamWriter(priceTxt))
            {
                for (var aux1 = 0; aux1 < listaPrices.Count; aux1++)
                {
                    sw.WriteLine($"{listaPrices[aux1]}");
                }
                sw.Close();
            }
            Console.WriteLine("OPERAÇÃO CONCLUÍDA!!!");
            Console.ReadKey();
        }

        else
        {
            Console.WriteLine("LISTA INEXISTENTE!");
        }

        Console.ReadKey();

    }

}
