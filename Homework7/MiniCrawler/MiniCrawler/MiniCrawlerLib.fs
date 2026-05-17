// <copyright file="MiniCrawlerLib.fs" author="Alina Letyagina">
// under MIT License.
// </copyright>

namespace MiniCrawler

open System.Net.Http
open System.Text.RegularExpressions

module MiniCrawler =

    /// Downloads the content of the given URL, finds all links in the format <a href="http://...">,
    /// downloads them in parallel, and prints the character count for each page.
    let downloadPageSizes (url: string) =
        async {
            use client = new HttpClient()

            try
                let! html = client.GetStringAsync(url) |> Async.AwaitTask

                let pattern = @"<a\s+(?:[^>]*?\s+)?href=""(?<url>http://[^""]+)"""
                let matches = Regex.Matches(html, pattern)

                let targetUrls = [ for m in matches -> m.Groups["url"].Value ] |> List.distinct

                printfn $"Found %d{targetUrls.Length} unique links. Starting parallel download..."

                let getPageInfo (targetUrl: string) =
                    async {
                        try
                            let! content = client.GetStringAsync(targetUrl) |> Async.AwaitTask
                            return Some(targetUrl, content.Length)
                        with ex ->
                            printfn $"Error downloading %s{targetUrl}: %s{ex.Message}"
                            return None
                    }

                let! results = targetUrls |> List.map getPageInfo |> Async.Parallel

                printfn "\nResults:"

                results
                |> Array.choose id
                |> Array.iter (fun (url, size) -> printfn $"%s{url} — %d{size}")

            with ex ->
                printfn $"Critical error: %s{ex.Message}"
        }
