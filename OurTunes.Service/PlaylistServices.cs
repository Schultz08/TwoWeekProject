﻿using OurTunes.Data;
using OurTunes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurTunes.Service
{
    public class PlaylistServices
    {
        public bool CreatePlaylist(PlaylistCreate model)
        {
            var entity =
                new Playlist()
                {
                    PlaylistName = model.PlaylistName,
                    UserId = model.UserId,
                    TotalTimeOfPlaylist = model.TotalTimeOfPlaylist
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Playlists.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

     public IEnumerable<PlaylistEdit> GetPlaylists()
          {
              using (var ctx = new ApplicationDbContext())
              {
                  var query =
                      ctx
                          .Playlists
                          .Where(e => e.PlaylistId == e.PlaylistId)
                          .Select(
                              e =>
                                  new PlaylistEdit
                                  {
                                      PlaylistId = e.PlaylistId,
                                      UserId = e.UserId,
                                      PlaylistName = e.PlaylistName,
                                      TotalTimeOfPlaylist = e.TotalTimeOfPlaylist
                                  }
                          );

                  return query.ToArray();
              }
          }

        public PlaylistEdit GetPlaylistByName(string name)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Playlists
                        .Single(e => e.PlaylistName == name);
                return
                    new PlaylistEdit
                    {
                        PlaylistName = entity.PlaylistName,
                        TotalTimeOfPlaylist = entity.TotalTimeOfPlaylist,
                    };
            }
        }

        public bool UpdatePlaylist(PlaylistEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Playlists
                    .Single(e => e.PlaylistId == model.PlaylistId);

                entity.PlaylistName = model.PlaylistName;
                entity.TotalTimeOfPlaylist = model.TotalTimeOfPlaylist;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeletePlaylist(int playlistId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Playlists
                    .Single(e => e.PlaylistId == playlistId);

                ctx.Playlists.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        //Song Methods

        public void PostSong(JointModel joint)
        {
            JointPlaylist addSong = new JointPlaylist();
            addSong.PlaylistId = joint.PlaylistId;
            addSong.SongId = joint.SongId;

            using (var context = new ApplicationDbContext())
            {
                context.JointPlaylists.Add(addSong);
                context.SaveChanges();
                
            };
        }
    }
}

