using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PictureViewer.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //=> options.UseSqlite(@"Data Source=pictureDatabase.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder
               .Entity<Customer>()
               .HasAlternateKey(c => c.Username);

            modelBuilder
                .Entity<Customer>()
                .HasMany(p => p.Pictures)
                .WithOne(t => t.Uploader);

            var firstCustomer = new Customer()
            {
                Id = 1,
                Username = "firstUser",
                Password = "asd"
            };
            var secondCustomer = new Customer()
            {
                Id = 2,
                Username = "second",
                Password = "asd",
            };
            var thirdCustomer = new Customer()
            {
                Id = 3,
                Username = "third",
                Password = "sad",
            };
            var fourthCustomer = new Customer()
            {
                Id = 4,
                Username = "fourth",
                Password = "asd",
            };


            modelBuilder.Entity<Customer>().HasData(
                firstCustomer,
                secondCustomer,
                thirdCustomer,
                fourthCustomer
            );


            var pictureBase64 = "iVBORw0KGgoAAAANSUhEUgAAACgAAAAoCAYAAACM/rhtAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAAEnQAABJ0Ad5mH3gAAAAjdEVYdEZpbGUARDpcQmxlbmRlclxQcm9qZWN0c1xiaXJkLmJsZW5kf5MStAAAABh0RVh0RGF0ZQAyMDIwLzA5LzE4IDIxOjI2OjI5KMLpkwAAABB0RVh0VGltZQAwMDowMDowMDowMJvEFlQAAAAJdEVYdEZyYW1lADAwMLZWJbQAAAANdEVYdENhbWVyYQBDYW1lcmFo/+/pAAAAC3RFWHRTY2VuZQBTY2VuZeUhXZYAAAATdEVYdFJlbmRlclRpbWUAMDI6MjkuNjP/M6W+AAAAHXRFWHRjeWNsZXMuVmlldyBMYXllci5zYW1wbGVzADEyOPLDV58AAAAldEVYdGN5Y2xlcy5WaWV3IExheWVyLnRvdGFsX3RpbWUAMDI6MjkuNTBPBpJBAAAAJnRFWHRjeWNsZXMuVmlldyBMYXllci5yZW5kZXJfdGltZQAwMjoyOC4xN7Q7U1gAAAAvdEVYdGN5Y2xlcy5WaWV3IExheWVyLnN5bmNocm9uaXphdGlvbl90aW1lADAwOjAxLjMyB/2gIgAADh1JREFUWEdtWFlzHFcZPdPTs6/SaLTYWmxLtiNDCFCQEEgKkhfgjUd+Tn4URRXwTJFKUUUSCC7bseVI1j6afenp7unhnO/OyFDFbbV6u/d+555vvZP65NP788//doFUCpjPgd/8+gmACHGcRRxN8Pnnr/D+B9t4c3yNra1NlIoNvHzxAp3eFCk/jVk8w+E7j/Dq1TEOHjTw1y+O8Gh/DUEQocc+48kU7zxexzdPLzkvkPU9BNEMHgUmCQXymuG7KE54SxBsftpDmuc0jOB1bijI3qfsGsXnmM2mODk5x5zHRx8/Qj5XRL1eRKlUNMEbWxuYBDEKOR8//eA+Li4vME8SfPvqxARcXvawUq9gRHCUg2q1sRCewmq9ZNdsNgPP80jK3AFdND3HM4LloX7e3e09Y24+T+yaTvmYkrmdnQ3kC3mMhkP0hy2sNddwdnaJbD5DkEOEZGE4ChESxOpKHRsbDTRWV0yATsw9FPNZXlO4bnVsMXpfqhQNSIryBMGB5LORtGxzzLhgNc/3OYm1BeZ0DrlMHmE4XAgbczLg5qaHKzLT7dwgCkP2SRtArtHYDoIAJ29uTFBjtYRgOqWZzBDyfP36As1G3aSUSyWT4/sZJIvFbK6XDeTblkIUxfbNs97WpFBwkFaa5gTsFE4wDRIU8kXU6nUy4vO9h0F3inI5S9UAFxdd2mGC0WhCNWuGFFqtPqq1KgrUgBCr3027Bw5Fv8+FSxqFazFS/TyJBIDfHUlCq/c6yHDaPtojX9I80brq0UgLuGkN0O1FOD654iQgyArBFrCyUkSvH5qQ/iDAcDzlfQppIWArl3Ko1IoEmDMQMwIXQ2nKOrtoWZ/ZbGbj1caT2GTL9twb4VEjg1Hk0C9XpTFxRMFk6c1JF9fXAZ+5iMSn4dcw6HfoIGNnyJz07laDakxwfdNHNpOxifbvb+Ho6DXZGt06QkhAUncuJ5MyaWTMfZvxcQlWwDSvPFnNGw2nduModW1tvY71TgYfNffx6Z0DHGa2CPQGZ+c3CMIpKtWqBpghy+Ol2vXmChmiEE5ycdnC5fmYWuDCJJxzyo5TVKHsUpIURqplOcyc9uybXBmZgMo2tRhN5j17dmTAjD31SiUonfoWpzJkxM9m4ac8bAdSa0BGsrQ3x7otaO6cbKW+YotUCwKyQsbq1YrWYf1SnF+n1C5JMb+L8RTnzi4AWuMAjwtJ81Tz5H0LUdZSXKqefMapTLkMP5+XqxvgJ8V1+JksgRdvVRLF1AC/HR+fmAOpPTjYxd52g/28W1MQez4ZVXiSvCKBtrt9OlDCOFl0i+AhOepndsvDk4ctuHMdXqRNHelcDplCAT6vAkAJxsDPfvobBuCBBVfZScJDn1dXysjm6JHs8/z5K4TTAFOeGYYjtSmBxckMiQHmM7OEQAuQADoEam7epTY8FxqWjVClClFM1YajIf7y9BskFXocj/rGJm3HR2MW8+omyHgZG1OrrtB23LvpdMbnMirSwIJVqcxnauQSJIZq1LrtiTFvYTJ8llOZNy805M34sGz2ih90VYcgE6FQreFPXx6ZKh5+/DFXDGyW87c2EjDEyBtP3pzSOcZmP5NpxEAdolKpMAJIRU5YGM6YUejpbBotdYuos7Mb9473kmsk2TMBS9fLlqM6998vm4FLRUkcMxBfmLc++NmHZNbD8Rd/ZjoEfn2wxoXMMRhOaORiYkbQpIXTyfAn05ggA+TzioXOa3WdBsw+vEYCzibsGdr18l5z3OZmvnD8s+UJrtlsWhiw8BHGuPvwMQNzDr/75H2s7Owg7LdQiIfGok7Z74OHe4yFKyjm8rh3b9WoieM5828fnXYXHk1C4lQcyLglW3anjsaU3WtR7lRoUphRM2+mq3+mh0KxiE6nA5KBZn0bdw4PsfnwED/+4Y+xsf0A0biPyy/+aAPVJOigUcSY6azdaTOjhHj85B7Lrkvs7tTIVIytjSauWlQfO2fpLDNGZDmAgrbP0BIrQrNJYxEX5VTrqhsxJ+jGoG4GgwEO7j/AB5/8Hg9/+Uus7O1xYMJajQyQqovP/2ArVGe3Wmcnq+02bU0lFPDq5ZGpkr6AaqVsBYQ0IrZz1IQAKP6JoIj2t7S3mJlIV4HX9yIXY2xyIGO/DtfWGg3TuxzCBtK7Ytqhns24DJL+8+CjA5qinbH6ocDdvbuoyYHoubLn/mDIwJzFRrPOfBvYnLJjjbFSbNE8BWutit+bpYIRU8x7+N7+hhiUONc0WICW57B7iUH7FElwjoNfidUdlFkX+synAqcJBftOMKUNyhmUGZRF5BQphpqqsaBaM0fvzWSdPWqs4qAEO4eJUefCatk0xizTVEnt31/Fv19eksGFELWX3z4jsAiDzinm4RnZYDFaZ3rKO7pW9w+w9e4PcO/nH2LznYcIpBqymzAAu4zhI0dn8FlTap5MJkcn8dAbjKwSV5AukaE7G+uWKdISTPmlQppF8RgjaiGhTt/9fg1nJx0rv+hQRoWt5IIFQXZ+gXolYSiJF8WAADiVC4yarK/EoL37ZB9DhhPJ+fAui06CUjTo91jkUm1H370h2DQePdpnyOE3Mqig/Ob8UrioVpb+dO6EDiKHaW75OPx+GScve5jSdktFplkTyM4CoDZltRwubY8GK3YEcOn6DqxAz1FsNNGaMSzwXusMaGcKhapElDW0FQi4ABU17GJy5ESLiZj66N15BnbGxHwhhcYaK6RRjNVmjhuxxxhMJm/joJqULca0U5PrS7DOJXvuugDMU2r66Fc/sV2axhXaHW6uqqx6RrS9NGtE7kVUVSM09Spo9PsTs/kq1VosMnBPJJXZaVMVTJoFxAw3I+Crp8+YdbxFmOHyNUgBVAa79FzHnGNLp6mbszlw7rtoicpVA6n7+pBbAHaqMMzICxRiBt2OxGBMZ8ryBYsc5Ap6Q+YTFb0ZRpA8o0YW7WHMeDylWdQwDBiibCTbUoUxAeremFTMojBjjkDddQGUp4Zo1JMfPEKHHjygOgVKuC+vWrqj56ZRX1mzCFFg+NHC7uyksb6Zwc52nt495zY2hdPTABenY5SLNRw+KeLZ8x48Zg3zYjVzFv5djlhKCdyCof933gLWIUo58IMP30OXIK9ZoX9yf4VaYHag4Xe7QxYD12jUqFJuPYtlD1sENmDR2xuG2H0gz/e5qUrQaGwiSkZ49mJEPyAYsnvLoFSsPew81bdobiDFFAHNaHdLwFLfW5DuXD5//Iv3cPj+uzjqhpxDGyGmMcsSAWbUzICZpXkXePrNGFftGZ2DTpL18PzphCHJx7OjYwb0iGY2x9pawTzbhRkJIj55oGwm5GTRwg4FzNS8BGZgnGr1T+bwFjB3dMUC3nBrsF1iGc+gvUFVpnORpbFSM8UqSfsN7mE20jh8XEWfzIVxinvs2GrHdFbZOMUsNEGaISjNfcdnok+TC+DGOmNPjxtzFpvLildrWDaBWTrO0k7/u6nr7p1Vqi3Eew8b6EZtS2OrBCdruLlWsKYNrum3n5COM8dN120Lcrk0JpMEdZpDmR6eYnhaqNghUMUh/P1ChlWx+2VA6nZAHBgx9latLnALrJqE6K7CffFCMWi35uj0Y5yczdBuz7G9y+qnlZhqmUton1wsU2OtynxOebW6QtEMvd4MGabL/3GSYjFrxWeW5X6ftVwwHrMKpk0oNwuo2DO7dABVPpkJGGgBchmJXWzJWtRHT3YIMOHmPuFGbI72VYK9ezm8fj3mvlvzSjaYDicWFydDZjECq1bJHeXcOokm1zZQeVMgwlwdE9ZyKt0F0hUQLoBH9uzuLSz9F5sCqN2cDNScjcB/+5N7aK5mkUtlqeKIIWWK/YMNerjMhB3UnSBHXEhq7mFvK4fcPE0Wl07Cpuq1XC7RmFkGpbiTYybvjaYYdXu0iymTvQOqKkSM6irw0cIMxKquphEBpkGrrFcZFrE6/9HeJgZUWzWfRqGcxldftjhHzGICKFZoGmRSVZAW9Oo4wGmLxQVxOSdZtIP9XZy+uUSpVrIfGqM5OyULkk2NApFYvjbAzNlLtS4dSm0ajAimZ3lY7A7GCuDAzmYVx+c95lhmH4YS1Y0F1rrBiHMT3LKlWEQUGQX0jgD9z7Rotb17W9i+s86BZJAGfH19jryXZfCkzbGTbGpqKteGyDmRmmo+A8lTgPrdG1y0BtwLz1nfMd6FzsECjhtQpSMC9hiFPNpawoA8Y5hZLrRWpYMGCVnnyXXRSZzd6HSViHb7E7PD4YC5kxWL8E8mIQ19wsn1a9bEqY6qFrMyIF1sAVR9tzvgOw9fvWjj62/bFgOnVPfzkwFabW7muf4sU2AsEFzEEpzalKzbLyEs1/R+YYOukwrEdovJnkI0KWio/V7HPFLfLdSQTd3zH9/zC8c7j55ZMdBqc3N1E+C8PeTmK49GtYCXJ11887KLIRcpM6hWfUYIFgKLn/44I6eTxc25fUgjy52gZZFbs1mwqPQ0oLvnC8ylSYRtbjUzPuMiKVdlrGYOZ6TJoKlCMjpgNdwfuPP0/JrqYcnUCRnPQrQHganZBmkcx3RafCYeaUkgREaOeVrqbXci9PV7oUToYL2mBZjAJ4dbpH2M3XuP3eroWqNxC9+9vML9+9yLzAsE6Bg0WTw1k8eQ//U/X9D46VR8qclnYtm+i2EywySg32mGLP3de2Gdc7fHbWjIvoqdhkofFAL5j39p3/fNi8VgpZxhyZ5FpbbKwYphDK5+gcGTXjno4B//OkOOHlissAjlqjXJ3//xAt8dX3F/MkOO8a9cyLEI0G85Js0JZZM8mYgY0z1NjPcqSBUZtEj3fqPZYISYuGCfSuE/sRnnS7YVRr0AAAAASUVORK5CYII=";

            var bytes = Convert.FromBase64String(pictureBase64);




            modelBuilder.Entity<Picture>()
                .HasData(
                    new Picture()
                    {
                        Id = 1,
                        Title = "First Picture",
                        Image = bytes,
                        UploaderId = 1
                    },
                    new Picture()
                    {
                        Id = 2,
                        Title = "Second Picture",
                        Image = bytes,
                        UploaderId = 1
                    },
                    new Picture()
                    {
                        Id = 3,
                        Title = "Third Picture",
                        Image = bytes,
                        UploaderId = 2
                    }
                );
        }
    }
}
