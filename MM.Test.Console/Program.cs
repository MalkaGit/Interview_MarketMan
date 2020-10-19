using MM.BL;
using System;

namespace MM.Test.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                //Application Start
                CelebsService celebsService = new CelebsService();
                CelebsService.ResetData();

                //GetAll
                var celebs = celebsService.GetAll();


                //Reset
                CelebsService.ResetData();

                bool Deleted1    = celebsService.Delete(3);
                var celebs2     = celebsService.GetAll();

                bool Deleted2   = celebsService.Delete(5);
                var celebs3     = celebsService.GetAll();



                CelebsService.ResetData();
                var celebs4 = celebsService.GetAll();


            }
            catch (Exception e)
            {
                int i = 5;
            }
          
            //Console.WriteLine("Hello World!");
        }
    }
}
