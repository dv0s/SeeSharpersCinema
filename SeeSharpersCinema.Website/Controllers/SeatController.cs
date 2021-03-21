﻿using Microsoft.AspNetCore.Mvc;
using SeeSharpersCinema.Data.Models.Program;
using SeeSharpersCinema.Data.Models.Repository;
using SeeSharpersCinema.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeeSharpersCinema.Website.Controllers
{
    public class SeatController : Controller
    {

        private IPlayListRepository playListRepository;
        private IReservedSeatRepository seatRepository;
        //private long TimeSlotId;
        bool COVID = true;

        public SeatController(IPlayListRepository playListRepository, IReservedSeatRepository seatRepository)
        {
            this.playListRepository = playListRepository;
            this.seatRepository = seatRepository;
        }

        [Route("Seat/Selector/{playListId}")]
        public async Task<IActionResult> Selector([FromRoute] long playListId)
        {
            var PlayListList = await playListRepository.FindAllAsync();
            var PlayList = PlayListList.FirstOrDefault(p => p.Id == playListId);

            //test all reserved seats
            var ReservedSeats = await seatRepository.FindAllByTimeSlotIdAsync(PlayList.TimeSlotId);

            //var Seats = ReservedSeats.Select(i => i.SeatId).ToList();
            var Seats = GetReservedSeats(ReservedSeats);

            //ViewData["ReservedSeats"] = Seats;
            ViewData["RoomCapacity"] = ReservedSeats.FirstOrDefault().TimeSlot.Room.Capacity;
            ViewData["PlaylistId"] = PlayList.Id;
            ViewData["ReservedSeats"] = Seats;
            //TimeSlotId = PlayList.TimeSlotId;

            return View(Seats);
        }

        /// <summary>
        /// SaveSeats puts the list of seat(Id)s gained form the seat selection in the seat repo context
        /// </summary>

        /*        public void SaveSeats(List<int> SeatList, int TimeSlotId)
                {
                    SeatList.ForEach(seat =>
                    {
                        ReservedSeat ReserevedSeat = new ReservedSeat { SeatId = seat, TimeSlotId = TimeSlotId };
                        seatRepository.AddItemAsync(ReserevedSeat);
                    });

                }
        */

        //todo remove once form is implemented
        public async Task SaveSeatTest()
        {
            List<int> List = new List<int> { 59, 60, 82, 83, 265, 266 };
            await SaveSeats(List, 3);
        }

        public async Task SaveSeats(List<int> SeatId, long TimeSlotId = 2)
        {
            List<ReservedSeat> ReservedSeat = new List<ReservedSeat>();
            SeatId.ForEach(s => {
                ReservedSeat.Add(
                new ReservedSeat { SeatId = s, TimeSlotId = TimeSlotId }
                );
            });
            await seatRepository.ReserveSeats(ReservedSeat);

        }


        public List<int> GetReservedSeats(IEnumerable<ReservedSeat> ReservedSeats)
        {
            var Seats = ReservedSeats.Select(i => i.SeatId).ToList();
            return Seats;

        }
    }
}
