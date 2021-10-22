using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LCK_players.Models;
using PagedList;
using PagedList.Mvc;
using System.Web.Security;

namespace LCK_players.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string txtUid, string txtPwd)
        {
            string[] uidAry = new string[] { "Faker", "Bang", "ShowMaker" };
            string[] pwdAry = new string[] { "666666", "ezjump", "imad" };

            // 循序搜尋法
            int index = -1;
            for (int i = 0; i < uidAry.Length; i++)
            {
                if (uidAry[i] == txtUid && pwdAry[i] == txtPwd)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                ViewBag.Err = "帳密錯誤!";
            }
            else
            {
                // 表單驗證服務，授權指定的帳號
                FormsAuthentication.RedirectFromLoginPage(txtUid, true);
                return RedirectToAction("Index");
            }
            return View();
        }
        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();   // 登出
            return RedirectToAction("Login");
        }

        // GET: Home
        int pageSize = 10;
        LCKEntities db = new LCKEntities();

        [Authorize(Users = "Faker")]
        public ActionResult Index(int  page= 1)
        {
            int currentPage = page < 1 ? 1 : page;
            var players = db.TableLCKs1081720.OrderBy(m => m.定位).ToList();
            var result = players.ToPagedList(currentPage, pageSize);
            return View(result);
        }

        //尋找隊伍
        public ActionResult SearchTeam()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Team(string team)
        {
            ViewBag.team = team;
            var info = db.TableLCKs1081720
                .Where(m => m.隊伍.Contains(team))
                .OrderBy(m => m.加入時間).OrderBy(m => m.先發)
                .ToList();


            return View(info);


        }
        //尋找相關選手
        public ActionResult SearchPlayer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Player(string team,string lineup,string five) 
        {
            ViewBag.team = team;
            ViewBag.lineup = lineup;
            ViewBag.five = five;
            //所有隊伍的特定位置的先發或候補
            if (team == "全部" && lineup != "全部" && five != "全部") 
            {
                ViewBag.note = "大家的" + lineup + five;
                var info = db.TableLCKs1081720
                .Where(m => m.先發 == lineup && m.定位 == five)
                .OrderBy(m => m.加入時間).OrderBy(m => m.先發)
                .ToList();

                return View(info);
            }
            //特定隊伍中特定位置的所有選手
            if(lineup == "全部" && team != "全部" && five != "全部")
            {
                ViewBag.note = team + "的" + five + "選手";
                var info = db.TableLCKs1081720
                .Where(m => m.隊伍 == team && m.定位 == five)
                .OrderBy(m => m.加入時間).OrderBy(m => m.先發)
                .ToList();

                return View(info);
            }
            //特定隊伍的所有先發或候補
            if (five == "全部" && lineup != "全部" && team != "全部")
            {
                ViewBag.note = team + "的" + lineup + "選手";
                var info = db.TableLCKs1081720
                .Where(m => m.隊伍 == team && m.先發 == lineup)
                .OrderBy(m => m.加入時間).OrderBy(m => m.先發)
                .ToList();

                return View(info);
            }
            //特定位置的所有先發或候補選手
           if (team == "全部" && lineup == "全部" && five!="全部") 
           {
                ViewBag.note = five + "選手們";
                var info = db.TableLCKs1081720
                .Where(m => m.定位 == five)
                .OrderBy(m => m.加入時間).OrderBy(m => m.先發)
                .ToList();

                return View(info);
            }
            //所有的先發或候補的選手
            if (team == "全部" && five == "全部" && lineup != "全部")
            {
                ViewBag.note = "大家的" + lineup + "選手";                
                var info = db.TableLCKs1081720
                .Where(m => m.先發 == lineup)
                .OrderBy(m => m.加入時間).OrderBy(m => m.先發)
                .ToList();

                return View(info);
            }
            //特定隊伍的所有選手
            if (lineup == "全部" && five == "全部" && team != "全部")
            {
                ViewBag.note = team + "的選手";
                var info = db.TableLCKs1081720
                .Where(m => m.隊伍 == team && m.先發!=null) 
                .OrderBy(m => m.加入時間).OrderBy(m => m.先發)
                .ToList();

                return View(info);
            }
            //所有的選手
            if (team == "全部" && five == "全部" && lineup=="全部")
            {
                ViewBag.note = "LCK所有的選手";
                var info = db.TableLCKs1081720
                .Where(m => m.先發!=null) 
                .OrderBy(m => m.加入時間).OrderBy(m => m.先發)
                .ToList();

                return View(info);
            }
            if(team != "全部" && five != "全部" && lineup != "全部") 
            {
                ViewBag.note = team + "的" + five +lineup;
                var info = db.TableLCKs1081720
                .Where(m => m.隊伍 == team && m.定位 == five && m.先發 == lineup)
                .OrderBy(m => m.加入時間).OrderBy(m => m.先發)
                .ToList();

                return View(info);
            }
            return View();
            
        }

        //編輯選手資料
        public ActionResult Edit(string playerId)
        {
            //id相等的資料顯示出來
            var player = db.TableLCKs1081720.Where(m => m.暱稱 == playerId).FirstOrDefault();
            return View(player);
        }

        [HttpPost]
        public ActionResult Edit(TableLCKs1081720 player)
        {
           

            if (ModelState.IsValid)
            {
                //var temp = db.Table0520s1081720.Where(m => m.fEmpId == employee.fEmpId).FirstOrDefault();
                var temp = (from m in db.TableLCKs1081720
                            where m.暱稱 == player.暱稱
                            select m).FirstOrDefault();

                ViewBag.name = player.暱稱;

                temp.暱稱 = player.暱稱;
                temp.譯名 = player.譯名;
                temp.韓文名 = player.韓文名;
                temp.定位 = player.定位;
                temp.先發 = player.先發;
                temp.生日 = player.生日;
                temp.國籍 = player.國籍;
                temp.加入時間 = player.加入時間;
                temp.隊伍 = player.隊伍;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(player);
        }

        public ActionResult Create()
        {
            return View();
        }
        //新增頁面 按下儲存
        [HttpPost]
        public ActionResult Create(TableLCKs1081720 player)
        {
            //有沒有送資料過來
            if (ModelState.IsValid)
            {
                ViewBag.Error = false;

                //找資料表裡有沒有與 輸入的編號 一樣的資料
                //var temp = db.Table0520s1081720.Where(m => m.fEmpId == employee.fEmpId).FirstOrDefault();
                var temp = (from m in db.TableLCKs1081720
                            where m.暱稱 == player.暱稱
                            select m).FirstOrDefault();

                //資料表裡有相同資料
                if (temp != null)
                {
                    ViewBag.Error = true;
                    return View(player);
                }

                db.TableLCKs1081720.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(player);
        }

        public ActionResult Delete(string playerId)
        {
            var player = db.TableLCKs1081720.Where(m => m.暱稱 == playerId).FirstOrDefault();
            db.TableLCKs1081720.Remove(player);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}