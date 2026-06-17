using Service;
using Model;
namespace TestApi1
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Apiservice api = new Apiservice();
            PersonList pList = await api.GetAllPerson();
            Console.WriteLine(pList.Count);
            int id = pList.Last().Id;
            await api.DeleteAPerson(id);
            pList = await api.GetAllPerson();
            Console.WriteLine(pList.Count);

            Person p1 = new Person() { First_name = "Naftali" };
            await api.InsertAPerson(p1);
            Person myPerson = pList.First();
            myPerson.First_name = "UpdatedName";
            await api.UpdateAPerson(myPerson);
            Console.WriteLine();


            GenderList gList = await api.GetAllGender();
            Console.WriteLine(gList.Count);
            id = gList.Last().Id;
            await api.DeleteAGender(id);
            gList = await api.GetAllGender();
            Console.WriteLine(gList.Count);

            Gender g1 = new Gender() { Gender_name = "NewGender" };
            await api.InsertAGender(g1);
            Gender myGender = gList.First();
            myGender.Gender_name = "UpdatedName";
            await api.UpdateAGender(myGender);
            Console.WriteLine();


            SubscriptionList sList = await api.GetAllSubscription();
            Console.WriteLine(sList.Count);
             id = sList.Last().Id;
            await api.DeleteASubscription(id);
            sList = await api.GetAllSubscription();
            Console.WriteLine(sList.Count);

            Subscription s1 = new Subscription() { Name_of_sub = "twice a day" };
            await api.InsertASubscription(s1);
            Subscription mySubscription = sList.First();
            mySubscription.Name_of_sub = "UpdatedName";
            await api.UpdateASubscription(mySubscription);
            Console.WriteLine();


            TrainerList tList = await api.GetAllTrainer();
            Console.WriteLine(tList.Count);
             id = tList.Last().Id;
            await api.DeleteATrainer(id);
            tList = await api.GetAllTrainer();
            Console.WriteLine(tList.Count);

            Trainer t1 = new Trainer() { First_name = "Natali" };
            await api.InsertATrainer(t1);
            Trainer myTrainer = tList.First();
            myTrainer.First_name = "UpdatedName";
            await api.UpdateATrainer(myTrainer);
            Console.WriteLine();


            TraineeList teList = await api.GetAllTrainee();
            Console.WriteLine(teList.Count);
             id = teList.Last().Id;
            await api.DeleteATrainee(id);
            teList = await api.GetAllTrainee();
            Console.WriteLine(teList.Count);

            Trainee te1 = new Trainee() { First_name = "Natali" };
            await api.InsertATrainee(te1);
            Trainee myTrainee = teList.First();
            myTrainee.First_name = "UpdatedName";
            myTrainee.PhotoPath = "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";
            int t=await api.UpdateATrainee(myTrainee);
            Console.WriteLine();



            Kinds_of_workoutsList kList = await api.GetAllKinds_of_workouts();
            Console.WriteLine(kList.Count);
             id = kList.Last().Id;
            await api.DeleteAKinds_of_workouts(id);
            kList = await api.GetAllKinds_of_workouts();
            Console.WriteLine(kList.Count);

            Kinds_of_workouts k1 = new Kinds_of_workouts() { Name_of_workout = "ice skating" };
            await api.InsertAKinds_of_workouts(k1);
            Kinds_of_workouts myKinds_of_workouts = kList.First();
            myKinds_of_workouts.Name_of_workout = "UpdatedName";
            await api.UpdateAKinds_of_workouts(myKinds_of_workouts);
            Console.WriteLine();




            Workouts_of_trainersList wList = await api.GetAllWorkouts_of_trainers();
            Console.WriteLine(wList.Count);
             id = wList.Last().Id;
            await api.DeleteAWorkouts_of_trainers(id);
            wList = await api.GetAllWorkouts_of_trainers();
            Console.WriteLine(wList.Count);

            Workouts_of_trainers w1 = new Workouts_of_trainers() { };
            await api.InsertAWorkouts_of_trainers(w1);
            Workouts_of_trainers myWorkouts_of_trainers = wList.First();
            myWorkouts_of_trainers.Id_trainer.First_name = "UpdatedName";
            await api.UpdateAWorkouts_of_trainers(myWorkouts_of_trainers);
            Console.WriteLine();



            Training_registrationList trList = await api.GetAllTraining_registration();
            Console.WriteLine("Before" + trList.Count);
             id = trList.Last().Id;
            await api.DeleteATraining_registration(id);
            trList = await api.GetAllTraining_registration();
            Console.WriteLine("After" + trList.Count);

            Training_registration tr1 = new Training_registration() { Id_trainee = new Trainee() { Id = 50 } };
            await api.InsertATraining_registration(tr1);
            Training_registration myTraining_registration = trList.First();
            myTraining_registration.Id_trainee.Telephone = "UpdatedName";
            await api.UpdateATraining_registration(myTraining_registration);
            Console.WriteLine();



            List_of_Exc_workoutsList leList = await api.GetAllList_of_Exc_workouts();
            Console.WriteLine(leList.Count);
             id = leList.Last().Id;
            await api.DeleteAList_of_Exc_workouts(id);
            leList = await api.GetAllList_of_Exc_workouts();
            Console.WriteLine(leList.Count);

            List_of_Exc_workouts le1 = new List_of_Exc_workouts() { };
            await api.InsertAList_of_Exc_workouts(le1);
            List_of_Exc_workouts myList_of_Exc_workouts = leList.First();
            myList_of_Exc_workouts.Id_kindOf_workouts.Name_of_workout = "UpdatedName";
            await api.UpdateAList_of_Exc_workouts(myList_of_Exc_workouts);
            Console.WriteLine();




            Console.ReadLine();
        }
    }
}
