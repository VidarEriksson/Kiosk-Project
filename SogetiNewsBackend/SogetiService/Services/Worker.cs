using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Refit;
using SogetiService.Data;
using Microsoft.EntityFrameworkCore;

namespace SogetiNewsConsoleTest
{
    internal class Worker(IServiceScopeFactory factory) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = factory.CreateScope();
                ISogetiNewsInterface service = scope
                  .ServiceProvider
                  .GetRequiredService<ISogetiNewsInterface>();
                var dbContext = scope.ServiceProvider.GetRequiredService<SogetiNewsDbContext>();

                var sogetinewss = await service.GetSogetiNews();
                var tags = sogetinewss.Tags.Where(n => n.ContentTypes.Contains("sortering")).ToList();

                foreach (Tag tag in tags)
                {
                    var existingTag = dbContext.Posts.FirstOrDefault(t => t.TagId == tag.ID);
                    if (existingTag is null)
                    {
                        dbContext.Posts.Add(new Post { Title = tag.Title, Intro = tag.Intro, Date = tag.Date, PageUrl = tag.PageUrl, ImageUrl = tag.ImageUrl, TagId = tag.ID });
                        await dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        existingTag.Title = tag.Title;
                        existingTag.Intro = tag.Intro;
                        existingTag.Date = tag.Date;
                        existingTag.PageUrl = tag.PageUrl;
                        existingTag.ImageUrl = tag.ImageUrl;
                        existingTag.TagId = tag.ID;
                    }
                    await dbContext.SaveChangesAsync();
                }

            await Task.Delay(10000, stoppingToken);
            }

        }
    }

}

