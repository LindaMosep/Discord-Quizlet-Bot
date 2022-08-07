using Discord;
using Discord.Rest;
using Discord.WebSocket;
using HtmlAgilityPack;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Collections.Generic;

public class ImageAttachment
{
    public int width;
    public int height;
    public string url;


}
public class UserWithChannel
{
    public SocketUser user;
    public SocketTextChannel channel;
    public float second;
    public SocketMessage message;
    public bool isProcessFinished;

    public UserWithChannel(SocketUser user, SocketTextChannel channel, float second, SocketMessage message, bool isProcessFinished)
    {
        this.user = user;
        this.channel = channel;
        this.second = second;
        this.message = message;
        this.isProcessFinished=isProcessFinished;
    }

}

internal static class Program
{
    public static DiscordSocketClient _client;

    public static bool isWorking = false;
    public static EmbedFooterBuilder SignFooter;
    public static int numOfFiles = 0;
    public static List<UserWithChannel> users = new List<UserWithChannel>();
    public static int FileCount;
    public static int FileCount2;
    public static CookieContainer cookie;
    public static void Main()
    {
        Console.WriteLine(Environment.CurrentDirectory);
        MainAsync().Wait();


    }
    static Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }

    public static async Task MainAsync()
    {

        _client = new DiscordSocketClient(new DiscordSocketConfig()
        { AlwaysDownloadUsers = true, GatewayIntents= GatewayIntents.All });

        SignFooter = new EmbedFooterBuilder().WithText("Powered by Meowdemia!").WithIconUrl("https://media.discordapp.net/attachments/917813714923683860/923807194640687134/Grey_Cute_Illustrated_Cat_and_Fish_Circle_Laptop_Sticker_1.png?width=559&height=559");




        // driver = new EdgeDriver(edgeOptions);

        _client.Log += Log;

        var cfg = new DiscordSocketConfig();




        await _client.LoginAsync(TokenType.Bot, "");
        await _client.StartAsync();


        await _client.SetGameAsync("LindaMosep!", "https://github.com/LindaMosep", ActivityType.Playing);
        _client.MessageReceived += TakeDown;
        _client.Ready += _client_Ready;
        await Task.Delay(-1);
    }

    private static async Task _client_Ready()
    {

        GetCookies();
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://quizlet.com/explanations/questions/use-these-parameters-fe10ec25-9713-48f5-a584-6ba52bc9d139");
        WebClient wb = new WebClient();

        request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";
        request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
        request.Method = "GET";
        request.Headers.Add("accept-encoding", "gzip, deflate, br");
        request.Headers.Add("accept-language", "tr-TR,tr;q=0.9,en-US;q=0.8,en;q=0.7");
        request.Headers.Add("cache-control", "max-age=0");
        request.Headers.Add("cookie", "qi5=1vjps8bmn6ugr%3ANGUQqhBb7ps6ea0Fi.2p; fs=r4nqbg; g_state={\"i_l\":0,\"i_t\":1641450624142}; qlts=1_lhu6R4PAxYa3X90HVM9vd8MFpqPzS5sTcaFBqC2jW06TzxBQ-gfpoofpGW.jDxycu8UrWdI3d1JHTg; qtkn=9H6deZMmdVSxjffeVMPtaq; _delighted_web={%22Nk3AkgdeccgO4tql%22:{%22_delighted_fst%22:{%22t%22:%221641364558631%22}}}; qmeasure__persistence=%7B%229%22%3A%2201101000%22%2C%223%22%3A%2200000001%22%7D; _ga=GA1.2.779385962.1642129880; __lt__cid=1f7117c9-1df0-4102-92b7-539fd492d036; __pdst=691ae05ca5134e668ae6bd39d838ed57; _rdt_uuid=1642129880014.dfddf80b-9db2-4b5c-8de8-c9da6dd8434e; _fbp=fb.1.1642129880086.643239117; __cfruid=39c21b42baf043b616d934ce171f0bec26c45957-1642228816; session_landing_page=Explanations%2FquestionDetail; app_session_id=232a8769-c756-4bed-8c4a-6df72652f3e8; akv=%7B%7D; ab.storage.deviceId.6f8c2b67-8bd5-42f6-9c5f-571d9701f693=%7B%22g%22%3A%22c4138411-b8f7-7e77-137b-10dfcfb76cc8%22%2C%22c%22%3A1641364436089%2C%22l%22%3A1642569443950%7D; ab.storage.userId.6f8c2b67-8bd5-42f6-9c5f-571d9701f693=%7B%22g%22%3A%22244752436%22%2C%22c%22%3A1641364436082%2C%22l%22%3A1642569443951%7D; __cf_bm=PwE155wy_zuoY0MDhtiiJ_x8yTNNkufviJVGxr6OZeQ-1642570458-0-AcHdEHZovRE0WPiVYXOJ9u8T+25y9386RQa16x4T1I+0k1rrzJYgOXFvYgUEzJF6ljqOFpKllrxYk4sOWZpFkJ0=; OptanonConsent=isGpcEnabled=1&datestamp=Wed+Jan+19+2022+08%3A46%3A12+GMT%2B0300+(GMT%2B03%3A00)&version=6.22.0&isIABGlobal=false&hosts=&consentId=58d4a151-861d-483e-9fd6-5859f325b6e7&interactionCount=1&landingPath=NotLandingPage&groups=C0001%3A1%2CN01%3A1%2CC0002%3A1%2CC0004%3A0&AwaitingReconsent=false; ab.storage.sessionId.6f8c2b67-8bd5-42f6-9c5f-571d9701f693=%7B%22g%22%3A%22ee9a8906-e395-248c-8d0c-77009f557be3%22%2C%22e%22%3A1642572972488%2C%22c%22%3A1642569443949%2C%22l%22%3A1642571172488%7D");

        try
        {
            var rp = await request.GetResponseAsync();
        }
        catch (WebException ex)
        {
            Console.WriteLine(ex.Status);
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.Response.ResponseUri);
            try
            {
                Console.WriteLine(ex.Data.Values.Cast<string>().ToList()[0]);
            }
            catch
            {
                Console.WriteLine("XD");
            }

        }

        Console.WriteLine("bla");

    }

    public static Task TakeDown(SocketMessage e)
    {

        MessageRecieve(e);

        return Task.CompletedTask;
    }

    public static async Task SlowMode(SocketGuildUser user, SocketTextChannel channel, SocketMessage e, bool isProcessFinished)
    {

        var uch = new UserWithChannel(user, channel, 300, e, false);
        users.Add(uch);

        while (!users.Find(m => m.user == uch.user && m.channel == uch.channel).isProcessFinished)
        {

            await Task.Delay(1);
        }


        for (int i = 300; i > 1; i--)
        {

            await Task.Delay(1);
            users.Find(m => m.user == uch.user && m.channel == uch.channel).second = i;


        }

        SlowModeFinished(users.Find(m => m.user == uch.user && m.channel == uch.channel).message);
        users.Remove(uch);

    }

    public static EmbedBuilder NotValidLink()
    {
        var embed = new EmbedBuilder().WithTitle("```Please send a valid url!```").WithColor(new Color(255, 0, 0)).WithDescription("The url you sent is not valid, please sent a valid url!")
            .WithThumbnailUrl("https://media.discordapp.net/attachments/917813714923683860/931391847996211200/b26d38dd88deb86ab2ae4c0de4d1785e.gif").WithFooter(SignFooter);

        return embed;


    }


    public static async Task<RestUserMessage> ProcessStarted(SocketMessage e)
    {
        var embed = new EmbedBuilder().WithTitle("Process started succesfully!").WithColor(new Color(255, 255, 0)).WithDescription(e.Author.Mention+ $" Please wait until process completed.")
                      .WithThumbnailUrl("https://media.discordapp.net/attachments/917813714923683860/927371809119174666/loader_backinout_1.gif").WithFooter(SignFooter);
        var msg = await e.Channel.SendMessageAsync("", false, embed.Build(), null, null, new MessageReference(e.Id));
        return msg;

    }

    public static EmbedBuilder ProcessCompleted(SocketMessage e)
    {
        var embed = new EmbedBuilder().WithTitle("Process completed succesfully!").WithColor(new Color(0, 255, 0)).WithDescription(e.Author.Mention+ $" Please check your DM!.")
                      .WithThumbnailUrl("https://media.discordapp.net/attachments/917813714923683860/927372219431129148/c3b6e85cfdddd49e731f27c31e4fc5e6_1.gif").WithFooter(SignFooter);
        return embed;
    }

    public static async Task SlowModeMessage(SocketMessage e, float second, SocketGuildUser user, SocketTextChannel channel)
    {
        var embed = new EmbedBuilder().WithTitle("Your in cooldown!").WithColor(new Color(255, 255, 0)).WithDescription(e.Author.Mention+ $" Your currently in cooldown, wait a **{second / 100:F2}** second.")
                      .WithThumbnailUrl("https://images-ext-1.discordapp.net/external/8pBdRwZ4cXwRdZPAoBhesOdgZa2EQMSznqHFGYt7iyg/https/cdn.dribbble.com/users/2015153/screenshots/6592242/progess-bar2.gif?width=745&height=559").WithFooter(SignFooter);
        users.Find(m => m.user == user && m.channel == channel).message = e;
        await e.Channel.SendMessageAsync("", false, embed.Build(), null, null, new MessageReference(e.Id));
    }

    public static async Task SlowModeFinished(SocketMessage e)
    {
        var embed = new EmbedBuilder().WithTitle("Your cooldown finished!").WithColor(new Color(0, 0, 255)).WithDescription(e.Author.Mention+ $" You can use me now.")
                      .WithThumbnailUrl("https://images-ext-2.discordapp.net/external/-xCsuG6EsrPfO15glJR33j2U57ztvS432HaW0D0oRV0/https/cdn.dribbble.com/users/1162077/screenshots/5427775/media/612968fb2a4690f4959deb23a00eb2d0.gif?width=745&height=559").WithFooter(SignFooter);
        try
        {
            await e.Author.SendMessageAsync("", false, embed.Build(), null, null);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    public static EmbedBuilder MessageError(SocketMessage e)
    {
        var embed = new EmbedBuilder();
        if ((e.Channel as IGuildChannel).Guild.GetRole(922634093471092737) != null)
        {
            embed = new EmbedBuilder().WithTitle("```It's seems there is an error!```").WithColor(new Color(255, 0, 0)).WithDescription(e.Author.Mention+ $" **Please report to {(e.Channel as IGuildChannel).Guild.GetRole(922634093471092737).Mention}**")
                   .WithThumbnailUrl("https://images-ext-2.discordapp.net/external/wOqzpmK--O8wLGpd1AHc4Bv2lBFebnihQSV0JpkcD7k/https/cdn.dribbble.com/users/2182116/screenshots/13933572/media/cc32730b1eb3a657a48a6ceacefc72fb.gif?width=745&height=559").WithFooter(SignFooter);

        }
        else
        {
            embed = new EmbedBuilder().WithTitle("```It's seems there is an error!```").WithColor(new Color(255, 0, 0)).WithDescription(e.Author.Mention+ $" **Please report to Admin!**")
                  .WithThumbnailUrl("https://images-ext-2.discordapp.net/external/wOqzpmK--O8wLGpd1AHc4Bv2lBFebnihQSV0JpkcD7k/https/cdn.dribbble.com/users/2182116/screenshots/13933572/media/cc32730b1eb3a657a48a6ceacefc72fb.gif?width=745&height=559").WithFooter(SignFooter);

        }

        return embed;

    }

    public static void GetCookies()
    {

        cookie = new CookieContainer();
        var msgs = _client.GetGuild(922634093445918751).GetTextChannel(931387519893897228).GetMessagesAsync(1).ToListAsync();
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(msgs.Result.ToList()[0].ToList()[0].Attachments.ToList()[0].Url);


        WebClient myWebClient = new WebClient();



        // Download the resource and load the bytes into a buffer.
        byte[] buffer = myWebClient.DownloadData(msgs.Result.ToList()[0].ToList()[0].Attachments.ToList()[0].Url);

        // Encode the buffer into UTF-8
        string download = Encoding.UTF8.GetString(buffer);
        var CookiesString = download;

        var CookieLines = Regex.Split(CookiesString, "},");
        foreach (var text in CookieLines)
        {
            var name = text.Substring(text.IndexOf("\"name\": \"") + "\"name\": \"".Length);
            name = name.Remove(name.IndexOf("\""));


            var value = text.Substring(text.IndexOf("\"value\": \"") + "\"value\": \"".Length);
            value = value.Remove(value.IndexOf("\""));

            var domain = text.Substring(text.IndexOf("\"domain\": \"") + "\"domain\": \"".Length);
            domain = domain.Remove(domain.IndexOf("\""));


            cookie.Add(new Cookie(name, value, "/", domain));
        }
    }

    public static void RemoveNodes(HtmlNodeCollection nodes)
    {
        if (nodes != null)
        {
            Console.WriteLine("test1");
            if (nodes.Count > 0)
            {
                Console.WriteLine("test12");
                foreach (var node in nodes)
                {
                    Console.WriteLine("test123");
                    node.Remove();
                }
            }
        }
    }

    public static void RemoveNode(HtmlNode nodes)
    {
        if (nodes != null)
        {
            Console.WriteLine("test1");
            nodes.Remove();
        }
    }
    public static async Task MessageRecieve(SocketMessage e)
    {
       
        if (e.Content.StartsWith("https://quizlet.com/"))
        {
            var user = (e.Author as IGuildUser);
            if (user != null)
            {
                var guild = _client.GetGuild((e.Author as IGuildUser).GuildId);
                if (guild != null)
                {
                    if (!users.Exists(m => m.user == e.Author as SocketGuildUser && m.channel == e.Channel as SocketTextChannel))
                    {

                        List<string> linksinmessage = new List<string>();
                        foreach (Match item in Regex.Matches(e.Content, @"(http|ftp|https):\/\/([\w\-_]+(?:(?:\.[\w\-_]+)+))([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?"))
                        {
                            linksinmessage.Add(item.Value);
                        }

                        if (linksinmessage.Count > 0)
                        {
                            if (FileCount >= 1000)
                            {
                                FileCount = 0;
                            }
                            #region Cancer
                            if (e.Content.StartsWith("https://quizlet.com/") && !e.Content.StartsWith("https://quizlet.com/explanations"))
                            {
                                var isUserHere = false;
                                if (!(e.Author as IGuildUser).RoleIds.ToList().Contains(925007529572962405))
                                {
                                    SlowMode(e.Author as SocketGuildUser, e.Channel as SocketTextChannel, e, false);
                                    isUserHere = true;
                                }
                                var startmsg = await ProcessStarted(e);
                                GetCookies();
                                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(linksinmessage[0]);
                                request.CookieContainer = cookie;
                                request.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36";
                                request.Timeout = 50000;

                                int m = 0;
                                WebResponse wbc = null;
                                try
                                {
                                    WebResponse wbb = request.GetResponse();
                                    wbc = wbb;



                                }
                                catch
                                {
                                    m = 5;
                                    if (isUserHere)
                                    {
                                        users.Find(m => m.user.Id == e.Author.Id && m.channel.Id == e.Channel.Id).isProcessFinished = true;
                                    }

                                    await startmsg.ModifyAsync(m => m.Embed = NotValidLink().Build());
                                }
                                GetCookies();


                                if (m == 5)
                                {
                                    if (isUserHere)
                                    {
                                        users.Find(m => m.user.Id == e.Author.Id && m.channel.Id == e.Channel.Id).isProcessFinished = true;
                                    }
                                }
                                else
                                {



                                    var stream = wbc.GetResponseStream();
                                    var sr = new StreamReader(stream);


                                    var data = sr.ReadToEnd();



                                    if (data.Contains("data-sourcename=\"mode-link-flashcards"))
                                    {

                                        int count = FileCount +1;
                                        FileCount++;
                                        data = data.Replace("href=\"/", "href=\"https://quizlet.com/");
                                        data = data.Replace(" src=\"/", "src=\"https://quizlet.com/");
                                        data = data.Replace("href=\"file:///", "href=\"https://quizlet.com/");
                                        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                                        doc.Load(new StringReader(data));

                                        var nodesToRemove1 = doc.DocumentNode.SelectNodes("//*[@id=\"MobileNavTarget\"]"); // mobile nav
                                        var nodesToRemove2 = doc.DocumentNode.SelectNodes("//*[@id=\"TopNavigationReactTarget\"]"); // normal nav


                                        Console.WriteLine(1);
                                        RemoveNodes(nodesToRemove1);
                                        Console.WriteLine(2);
                                        RemoveNodes(nodesToRemove2);
                                        Console.WriteLine(3);
                                        var nodesToRemove3 = doc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[1]/div"); // unspport nav
                                        RemoveNode(nodesToRemove3);


                                        var nodesToRemove4 = doc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[2]/div[2]/div"); // sticky



                                        Console.WriteLine(4);
                                        RemoveNode(nodesToRemove4);


                                        var nodesToRemove5 = doc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[2]/footer/div[1]/div/div/div"); // sticky



                                        Console.WriteLine(5);
                                        RemoveNode(nodesToRemove5);


                                        var nodesToRemove6 = doc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[2]/footer"); // sticky



                                        Console.WriteLine(6);
                                        RemoveNode(nodesToRemove6);



                                        FileStream sw = new FileStream(Environment.CurrentDirectory + $@"\Meowdemia_QuizletFlash({count}).html", FileMode.Create);
                                        doc.Save(sw);
                                        while (!File.Exists(Environment.CurrentDirectory  + $@"\Meowdemia_QuizletFlash({count}).html"))
                                        {
                                            int c = 0;
                                            c++;
                                        }

                                        sw.Close();




                                        FileInfo bla = new FileInfo(Environment.CurrentDirectory +$@"\Meowdemia_QuizletFlash({count}).html");

                                        if (bla.Length == 0)
                                        {

                                            await startmsg.ModifyAsync(m => m.Embed = MessageError(e).Build());
                                            File.Delete(Environment.CurrentDirectory +$@"\Meowdemia_QuizletFlash({count}).html");

                                            if (isUserHere)
                                            {
                                                users.Find(m => m.user.Id == e.Author.Id && m.channel.Id == e.Channel.Id).isProcessFinished = true;
                                            }
                                        }
                                        else
                                        {

                                            var msg = await _client.GetUser(411997699647340545).SendFileAsync(Environment.CurrentDirectory + $@"\Meowdemia_QuizletFlash({count}).html");

                                            string urlOfFile = "";
                                            foreach (var mg in msg.Attachments)
                                            {
                                                urlOfFile = mg.Url;
                                            }


                                            File.Delete(Environment.CurrentDirectory  +$@"\Meowdemia_QuizletFlash({count}).html");

                                            EmbedBuilder TakeThis = new EmbedBuilder().WithTitle("Here it is, best postman in the whole World!").WithColor(Color.Green).AddField("Could you open the door please? I got some documentation for you. Study well!", $"[Download!]({urlOfFile})")
                                                                                                                            .WithThumbnailUrl("https://images-ext-1.discordapp.net/external/LUCi0IWmG8Jd-2yZlGW2Z1IHes2_A5KOuOfnV1KecwQ/https/cdn.dribbble.com/users/1616371/screenshots/6042217/media/844e55475e98ada8d64e1166e4d5a1b1.gif").WithFooter(SignFooter);
                                            await e.Author.SendMessageAsync(null, false, TakeThis.Build());
                                            await startmsg.ModifyAsync(m => m.Embed = ProcessCompleted(e).Build());
                                            if (isUserHere)
                                            {
                                                users.Find(m => m.user.Id == e.Author.Id && m.channel.Id == e.Channel.Id).isProcessFinished = true;
                                            }
                                        }

                                    }
                                    else
                                    {

                                        await startmsg.ModifyAsync(m => m.Embed = NotValidLink().Build());
                                        if (isUserHere)
                                        {
                                            users.Find(m => m.user.Id == e.Author.Id && m.channel.Id == e.Channel.Id).isProcessFinished = true;
                                        }
                                    }

                                }




                            }


                            #endregion
                            if (e.Content.StartsWith("https://quizlet.com/explanations"))
                            {

                                if (FileCount2 >= 1000)
                                {
                                    FileCount2 = 0;
                                }
                                GetCookies();
                                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(linksinmessage[0]);
                                WebClient wb = new WebClient();


                                request.Method = "GET";
                                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";
                                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
                                request.CookieContainer = cookie;
                                request.Timeout = 50000;
                                var startmsg = await ProcessStarted(e);
                                var isUserHere = false;
                                if (!(e.Author as IGuildUser).RoleIds.ToList().Contains(925007529572962405))
                                {
                                    SlowMode(e.Author as SocketGuildUser, e.Channel as SocketTextChannel, e, false);
                                    isUserHere = true;
                                }
                                var count = FileCount2 + 1;
                                FileCount2++;

                                try
                                {
                                    var response = request.GetResponse();

                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message + " Babanae");
                                }
                                try
                                {




                                    wb.Headers.Add(HttpRequestHeader.Cookie, cookie.GetCookieHeader(new Uri(linksinmessage[0])));
                                    wb.Headers.Add("user-agent", "Other");
                                    Console.WriteLine(0);
                                    var data = wb.DownloadString(linksinmessage[0]);
                                    Console.WriteLine(1);
                                    if (!data.Contains("The page you&#x27;re looking for is outdated, or just isn&#x27;t a thing"))
                                    {
                                        wb.Dispose();
                                        try
                                        {

                                            data = data.Replace("href=\"", "href=\"https://quizlet.com");
                                            data = data.Replace("src=\"/", "href=\"https://quizlet.com/");
                                            data = data.Replace("abuu5sv", "abuu5sv r8gl7vf");
                                            string json = "";
                                            data = data.Replace("href=\"https://quizlet.com/_next/static/media/PlusBadge.51a97180af9087fb894562f82efdafbb.svg\"", "src=\"https://quizlet.com/_next/static/media/PlusBadge.51a97180af9087fb894562f82efdafbb.svg\"");
                                            if (data.IndexOf("<script id=\"__NEXT_DATA__") != -1)
                                            {
                                                json = data.Substring(data.IndexOf("<script id=\"__NEXT_DATA__"));
                                            }
                                            else
                                            {
                                                json = "null";
                                            }

                                            if (json != "null")
                                            {
                                                data = data.Replace(json, "");
                                            }

                                            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                                            doc.Load(new StringReader(data));

                                            #region Remove Nodes
                                            var nodesToRemove = doc.DocumentNode.SelectNodes("//*[@id=\"__next\"]/div/div/div");
                                            var nodesToRemove1 = doc.DocumentNode.SelectNodes("//*[@id=\"__next\"]/div/div/main/div/div/div/div/div/div[2]/div[1]/div[2]/div");
                                            var nodesToRemove2 = doc.DocumentNode.SelectNodes("//*[@id=\"__next\"]/div/div/main/div/div/div/main/div/section");
                                            var nodesToRemove3 = doc.DocumentNode.SelectNodes("//*[@id=\"__next\"]/div/div/main/div/div/div/div/div/div[2]/div[1]/div[2]/div");
                                            var nodesToRemove4 = doc.DocumentNode.SelectNodes("//*[@id=\"__next\"]/div/div/main/div/div/div/main/div/section");
                                            var nodesToRemove5 = doc.DocumentNode.SelectNodes("//*[@id=\"__next\"]/div/footer");
                                            var nodesToRemove6 = doc.DocumentNode.SelectNodes("//*[@id=\"__next\"]/div/div/main/div/div/div/main/div/div[2]");
                                            var nodesToRemove7 = doc.DocumentNode.SelectNodes("//*[@id=\"__next\"]/div/div/main/div/div/div/main/div/div/div/div[2]/div/div[2]");
                                            var nodesToRemove8 = doc.DocumentNode.SelectNodes("//*[@id=\"__next\"]/div/div/main/div/div/div/main/div/div/div/div[1]/span/span[1]/svg");

                                            if (nodesToRemove != null)
                                            {
                                                foreach (var node in nodesToRemove)
                                                {
                                                    node.Remove();
                                                }
                                            }

                                            if (nodesToRemove1 != null)
                                            {
                                                foreach (var node in nodesToRemove1)
                                                {
                                                    node.Remove();
                                                }
                                            }

                                            if (nodesToRemove2 != null)
                                            {
                                                foreach (var node in nodesToRemove2)
                                                {
                                                    node.Remove();
                                                }
                                            }

                                            if (nodesToRemove3!= null)
                                            {
                                                foreach (var node in nodesToRemove3)
                                                {
                                                    node.Remove();
                                                }
                                            }

                                            if (nodesToRemove4 != null)
                                            {
                                                foreach (var node in nodesToRemove4)
                                                {
                                                    node.Remove();
                                                }

                                            }

                                            if (nodesToRemove5 != null)
                                            {
                                                foreach (var node in nodesToRemove5)
                                                {
                                                    node.Remove();
                                                }
                                            }

                                            if (nodesToRemove6 != null)
                                            {

                                                foreach (var node in nodesToRemove6)
                                                {
                                                    node.Remove();
                                                }
                                            }

                                            if (nodesToRemove7 != null)
                                            {
                                                foreach (var node in nodesToRemove7)
                                                {
                                                    node.Remove();
                                                }
                                            }

                                            if (nodesToRemove8 != null)
                                            {
                                                foreach (var node in nodesToRemove8)
                                                {
                                                    node.Remove();
                                                }
                                            }









                                            #endregion
                                            int StepCount = 0;
                                            for (int i = 1; i < 500; i++)
                                            {
                                                if (doc.DocumentNode.SelectSingleNode($"//*[@id=\"__next\"]/div/div/main/div/div/div/main/div/div/div/div[2]/div/div/div[{i}]") == null)
                                                {
                                                    break;
                                                }
                                                else
                                                {
                                                    StepCount++;
                                                }
                                            }

                                            if (json != "null")
                                            {

                                                for (int i = 0; i < StepCount; i++)
                                                {

                                                    if (json.Contains($"\"stepNumber\":{i}"))
                                                    {

                                                        var subjs = json.Remove(json.IndexOf($"\"stepNumber\":{i}"));

                                                        if (subjs.LastIndexOf("\"images\":") != -1)
                                                        {
                                                            var newsub = subjs.Substring(subjs.LastIndexOf("\"images\":"));
                                                            if (newsub.IndexOf("\"stepNumber\":") == -1)
                                                            {

                                                                if (newsub.IndexOf("\"additional\":") != -1)
                                                                {

                                                                    var addiotinal = newsub.Substring(newsub.IndexOf("\"additional\":"));

                                                                    List<string> addiotinallinks = new List<string>();
                                                                    foreach (Match item in Regex.Matches(addiotinal, @"(http|ftp|https):\/\/([\w\-_]+(?:(?:\.[\w\-_]+)+))([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?"))
                                                                    {
                                                                        addiotinallinks.Add(item.Value);
                                                                    }

                                                                    if (addiotinallinks.Count > 0)
                                                                    {

                                                                        var image = new ImageAttachment();
                                                                        image.url = addiotinallinks[0];
                                                                        if (addiotinal.IndexOf("\"height\":") != -1)
                                                                        {
                                                                            var heightsub = addiotinal.Substring(addiotinal.IndexOf("\"height\":") + "\"height\":".Length);
                                                                            var parseindex = -1;
                                                                            var height = -1;
                                                                            for (int c = 0; c < heightsub.Length; c++)
                                                                            {
                                                                                if (int.TryParse(heightsub.Remove(c), out height))
                                                                                {
                                                                                    parseindex = c;
                                                                                }

                                                                                if (parseindex != -1 && !int.TryParse(heightsub.Remove(c), out height))
                                                                                {
                                                                                    break;
                                                                                }
                                                                            }

                                                                            if (parseindex != -1)
                                                                            {
                                                                                height = int.Parse(heightsub.Remove(parseindex));
                                                                            }



                                                                            image.height = height;
                                                                        }

                                                                        if (addiotinal.IndexOf("\"width\":") != -1)
                                                                        {
                                                                            var widthsub = addiotinal.Substring(addiotinal.IndexOf("\"width\":") + "\"width\":".Length);
                                                                            var parseindex = -1;
                                                                            var width = -1;

                                                                            for (int c = 0; c < widthsub.Length; c++)
                                                                            {
                                                                                if (int.TryParse(widthsub.Remove(c), out width))
                                                                                {
                                                                                    parseindex = c;
                                                                                }

                                                                                if (parseindex != -1 && !int.TryParse(widthsub.Remove(c), out width))
                                                                                {
                                                                                    break;
                                                                                }
                                                                            }

                                                                            if (parseindex != -1)
                                                                            {
                                                                                width = int.Parse(widthsub.Remove(parseindex));
                                                                            }

                                                                            image.width = width;

                                                                        }


                                                                        if (image.width != -1)
                                                                        {
                                                                            var mnNode = doc.DocumentNode.SelectSingleNode($"//*[@id=\"__next\"]/div/div/main/div/div/div/main/div/div/div/div[2]/div/div/div[{i}]");

                                                                            if (mnNode != null)
                                                                            {
                                                                                var childs = new List<HtmlNode>();
                                                                                childs.AddRange(mnNode.ChildNodes.ToList());
                                                                                for (int aa = 0; aa < 5000; aa++)
                                                                                {
                                                                                    List<HtmlNode> tempNodes = new List<HtmlNode>();
                                                                                    int temp = 0;
                                                                                    foreach (var child in childs.ToList())
                                                                                    {
                                                                                        if (child.ChildNodes.Count > 0)
                                                                                        {
                                                                                            if (!childs.Contains(child.ChildNodes[0]))
                                                                                            {
                                                                                                tempNodes.AddRange(child.ChildNodes.ToList());
                                                                                                temp++;
                                                                                            }
                                                                                        }


                                                                                    }

                                                                                    if (temp == 0)
                                                                                    {
                                                                                        break;
                                                                                    }

                                                                                    childs.AddRange(tempNodes);
                                                                                }

                                                                                HtmlNode imageNode = HtmlNode.CreateNode($"<div class=\"Image\"><img alt=\"step\" class=\"Image-image\" referrerpolicy=\"no-referrer\" src=\"{ image.url}\" width=\"{image.width}\">");
                                                                                for (int test = 0; test < childs.Count; test++)
                                                                                {
                                                                                    if (childs[test].Attributes["class"] != null)
                                                                                    {

                                                                                        var cn = childs[test].GetAttributeValue("class", "default");

                                                                                        if (cn.ToLower().Trim() == "s1xkd811")
                                                                                        {

                                                                                            childs[test].AppendChild(imageNode);
                                                                                        }
                                                                                    }
                                                                                }




                                                                            }

                                                                        }


                                                                    }
                                                                }

                                                                if (newsub.IndexOf("\"latex\":") != -1)
                                                                {

                                                                    if (newsub.IndexOf("\"large\":") != -1)
                                                                    {

                                                                        var largetest = newsub.Substring(newsub.IndexOf("\"large\":"));


                                                                        for (int k = 0; k < 55; k++)
                                                                        {

                                                                            if (largetest.Contains(("\"large\":")))
                                                                            {

                                                                                largetest = largetest.Substring(largetest.IndexOf("\"large\":") + "\"large\":".Length);
                                                                                List<string> largelinks = new List<string>();
                                                                                foreach (Match item in Regex.Matches(largetest, @"(http|ftp|https):\/\/([\w\-_]+(?:(?:\.[\w\-_]+)+))([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?"))
                                                                                {
                                                                                    largelinks.Add(item.Value);
                                                                                }

                                                                                if (largelinks.Count > 0)
                                                                                {


                                                                                    var image = new ImageAttachment();
                                                                                    image.url = largelinks[0];


                                                                                    if (largetest.IndexOf("\"height\":") != -1)
                                                                                    {
                                                                                        var heightsub = largetest.Substring(largetest.IndexOf("\"height\":") + "\"height\":".Length);
                                                                                        var parseindex = -1;
                                                                                        var height = -1;
                                                                                        for (int c = 0; c < heightsub.Length; c++)
                                                                                        {
                                                                                            if (int.TryParse(heightsub.Remove(c), out height))
                                                                                            {
                                                                                                parseindex = c;
                                                                                            }

                                                                                            if (parseindex != -1 && !int.TryParse(heightsub.Remove(c), out height))
                                                                                            {
                                                                                                break;
                                                                                            }
                                                                                        }

                                                                                        if (parseindex != -1)
                                                                                        {
                                                                                            height = int.Parse(heightsub.Remove(parseindex));
                                                                                        }



                                                                                        image.height = height;
                                                                                    }

                                                                                    if (largetest.IndexOf("\"width\":") != -1)
                                                                                    {
                                                                                        var widthsub = largetest.Substring(largetest.IndexOf("\"width\":") + "\"width\":".Length);
                                                                                        var parseindex = -1;
                                                                                        var width = -1;
                                                                                        for (int c = 0; c < widthsub.Length; c++)
                                                                                        {
                                                                                            if (int.TryParse(widthsub.Remove(c), out width))
                                                                                            {
                                                                                                parseindex = c;
                                                                                            }

                                                                                            if (parseindex != -1 && !int.TryParse(widthsub.Remove(c), out width))
                                                                                            {
                                                                                                break;
                                                                                            }
                                                                                        }

                                                                                        if (parseindex != -1)
                                                                                        {
                                                                                            width = int.Parse(widthsub.Remove(parseindex));
                                                                                        }



                                                                                        image.width = width;
                                                                                    }


                                                                                    if (image.width != -1)
                                                                                    {

                                                                                        var mnNode = doc.DocumentNode.SelectSingleNode($"//*[@id=\"__next\"]/div/div/main/div/div/div/main/div/div/div/div[2]/div/div/div[{i}]");

                                                                                        if (mnNode != null)
                                                                                        {
                                                                                            var childs = new List<HtmlNode>();
                                                                                            childs.AddRange(mnNode.ChildNodes.ToList());


                                                                                            for (int aa = 0; aa < 5000; aa++)
                                                                                            {
                                                                                                List<HtmlNode> tempNodes = new List<HtmlNode>();
                                                                                                int temp = 0;
                                                                                                foreach (var child in childs.ToList())
                                                                                                {
                                                                                                    if (child.ChildNodes.Count > 0)
                                                                                                    {
                                                                                                        if (!childs.Contains(child.ChildNodes[0]))
                                                                                                        {
                                                                                                            tempNodes.AddRange(child.ChildNodes.ToList());
                                                                                                            temp++;
                                                                                                        }
                                                                                                    }


                                                                                                }

                                                                                                if (temp == 0)
                                                                                                {
                                                                                                    break;
                                                                                                }

                                                                                                childs.AddRange(tempNodes);
                                                                                            }
                                                                                            HtmlNode imageNode = HtmlNode.CreateNode($"<div class=\"Image\"><img alt=\"step\" class=\"Image-image\" referrerpolicy=\"no-referrer\" src=\"{ image.url}\" width=\"{image.width}\">");
                                                                                            for (int test = 0; test < childs.Count; test++)
                                                                                            {
                                                                                                if (childs[test].Attributes["class"] != null)
                                                                                                {

                                                                                                    var cn = childs[test].GetAttributeValue("class", "default");

                                                                                                    if (cn.Trim().Contains("isLatexImage"))
                                                                                                    {
                                                                                                        childs[test].AppendChild(imageNode);
                                                                                                    }
                                                                                                }
                                                                                            }




                                                                                        }




                                                                                    }
                                                                                    largetest = largetest.Remove(largetest.IndexOf(largelinks[0]));

                                                                                }
                                                                                else
                                                                                {
                                                                                    break;
                                                                                }




                                                                            }
                                                                            else
                                                                            {
                                                                                break;
                                                                            }

                                                                        }

                                                                    }



                                                                }
                                                            }
                                                        }

                                                    }



                                                }
                                            }


                                            FileStream sw = new FileStream(Environment.CurrentDirectory + $@"\Meowdemia_Explanation({count}).html", FileMode.Create);
                                            doc.Save(sw);
                                            while (!File.Exists(Environment.CurrentDirectory  + $@"\Meowdemia_Explanation({count}).html"))
                                            {
                                                int c = 0;
                                                c++;
                                            }

                                            sw.Close();



                                            var msg = await _client.GetUser(411997699647340545).SendFileAsync(Environment.CurrentDirectory +  $@"\Meowdemia_Explanation({count}).html");

                                            string urlOfFile = "";
                                            foreach (var mg in msg.Attachments)
                                            {
                                                urlOfFile = mg.Url;
                                            }


                                            File.Delete(Environment.CurrentDirectory  + $@"\Meowdemia_Explanation({count}).html");

                                            EmbedBuilder TakeThis = new EmbedBuilder().WithTitle("Here it is, best postman in the whole World!").WithColor(Color.Green).AddField("Could you open the door please? I got some documentation for you. Study well!", $"[Download!]({urlOfFile})")
                                                                                                                            .WithThumbnailUrl("https://images-ext-1.discordapp.net/external/LUCi0IWmG8Jd-2yZlGW2Z1IHes2_A5KOuOfnV1KecwQ/https/cdn.dribbble.com/users/1616371/screenshots/6042217/media/844e55475e98ada8d64e1166e4d5a1b1.gif").WithFooter(SignFooter);
                                            await e.Author.SendMessageAsync(null, false, TakeThis.Build());
                                            await startmsg.ModifyAsync(m => m.Embed = ProcessCompleted(e).Build());
                                            if (isUserHere)
                                            {
                                                users.Find(m => m.user.Id == e.Author.Id && m.channel.Id == e.Channel.Id).isProcessFinished = true;
                                            }


                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);

                                            if (isUserHere)
                                            {
                                                users.Find(m => m.user.Id == e.Author.Id && m.channel.Id == e.Channel.Id).isProcessFinished = true;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        await startmsg.ModifyAsync(m => m.Embed = NotValidLink().Build());

                                        if (isUserHere)
                                        {
                                            users.Find(m => m.user.Id == e.Author.Id && m.channel.Id == e.Channel.Id).isProcessFinished = true;
                                        }

                                    }






                                }
                                catch (Exception em)
                                {
                                    Console.WriteLine(em.Message);


                                    if (isUserHere)
                                    {
                                        users.Find(m => m.user.Id == e.Author.Id && m.channel.Id == e.Channel.Id).isProcessFinished = true;
                                    }
                                }
                            }






                        }

                    }
                    else
                    {

                        if (users.Find(m => m.user == e.Author as SocketGuildUser && m.channel == e.Channel as SocketTextChannel) != null)
                        {
                            if (users.Find(m => m.user == e.Author as SocketGuildUser && m.channel == e.Channel as SocketTextChannel).isProcessFinished)
                            {
                                await SlowModeMessage(e, users.Find(m => m.user == e.Author as SocketGuildUser && m.channel == e.Channel as SocketTextChannel).second, e.Author as SocketGuildUser, e.Channel as SocketTextChannel);
                            }

                        }

                    }


                }
            }

        }
    }

}
