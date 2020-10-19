Hi,
please find the project at: https://github.com/MalkaGit/MarketMan.git.

let me know if you have any issue dowloading\running it.

Thanks,
Regine.



Notes
1.   when json does not exist we load data from imdb automatically.
     see: static constructor of CelebsRepositry.
     operation is long (about  40 sec).
     the reason is that we load extra page for each celeb to get its birthdate.
     
2.   to imporve perforance we use multiple threads when we load data from imdb
     (see webScrabber::Get_Celebs_From_WebSite).

3.   to improve perofrmance on "Reset data" we load data from backup json file
     see CelebsRepositry::ResetData

4. log fiels at: MarketMan\MM.App\Logs
5. json files at MarketMan\MM.App\bin\Debug\netcoreapp3.1.   
	celebs_data.json           
	celevs_data.backup.json  


