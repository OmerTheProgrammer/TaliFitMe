using Microsoft.AspNetCore.Mvc;
using Model;
using ViewModel;

namespace Fitali_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SelectController : ControllerBase
    {
        [HttpGet]
        [ActionName("GenderSelector")]
        public GenderList SelectAllGender()
        {
            GenderDB db=new GenderDB();
            GenderList genders=db.SelectAll();
            return genders;
        }
        [HttpPost]
        [ActionName("InsertAGender")]
        public int InsertAGender([FromBody] Gender gender)
        {
            GenderDB db= new GenderDB();
            db.Insert(gender);
            int x = db.SaveChanges();
            return x;
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteAGender")]
        public int DeleteAGender(int id)
        {
            Gender gender = GenderDB.SelectById(id);
            GenderDB db = new GenderDB();
            db.Delete(gender);
            int x = db.SaveChanges();
            return x;
        }
        [HttpPut]
        [ActionName("UpdateAGender")]
        public void UpdateAGender([FromBody] Gender gender)
        {
            GenderDB db=new GenderDB();
            db.Update(gender);
            int x = db.SaveChanges();
        }


        [HttpGet]
        [ActionName("ManagerSelector")]
        public ManagerList SelectAllManager()
        {
            ManagerDB db = new ManagerDB();
            ManagerList manager = db.SelectAll();
            return manager;
        }
        [HttpPost]
        [ActionName("InsertAManager")]
        public int InsertAManager([FromBody] Manager manager)
        {
            ManagerDB db = new ManagerDB();
            db.Insert(manager);
            int x = db.SaveChanges();
            return x;
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteAManager")]
        public int DeleteAManager(int id)
        {
            Manager manager = ManagerDB.SelectById(id);
            ManagerDB db = new ManagerDB();
            db.Delete(manager);
            int x = db.SaveChanges();
            return x;
        }

        [HttpPut]
        [ActionName("UpdateAManager")]
        public void UpdateAManager([FromBody] Manager manager)
        {
            ManagerDB db = new ManagerDB();
            db.Update(manager);
            int x = db.SaveChanges();
        }

        [HttpGet]
        [ActionName("PersonSelector")]

        public PersonList SelectAllPerson()
        {
            PersonDB db = new PersonDB();
            PersonList persons = db.SelectAll();
            return persons;
        }
        [HttpPost]
        [ActionName("InsertAPerson")]
        public int InsertAPerson(Person p)
        {
            PersonDB db = new PersonDB();
            db.Insert(p);
            int x = db.SaveChanges();
            return x;
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteAPerson")]
        public int DeleteAPerson(int id)
        {
            Person person = PersonDB.SelectById(id);
            PersonDB db = new PersonDB();
            db.Delete(person);
            int x = db.SaveChanges();
            return x;
        }
        [HttpPut]
        [ActionName("UpdateAPerson")]
        public void UpdateAPerson([FromBody] Person person)
        {
            PersonDB db = new PersonDB();
            db.Update(person);
            int x = db.SaveChanges();
        }

        [HttpGet]
        [ActionName("SubscriptionSelector")]
        public SubscriptionList SelectAllSubscription()
        {
            SubscriptionDB db = new SubscriptionDB();
            SubscriptionList subscription = db.SelectAll();
            return subscription;
        }
        [HttpPost]
        [ActionName("InsertASubscription")]
        public int InsertASubscription(Subscription s)
        {
            SubscriptionDB db = new SubscriptionDB();
            db.Insert(s);
            int x = db.SaveChanges();
            return x;
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteASubscription")]
        public int DeleteASubscription(int id)
        {
            Subscription subscription = SubscriptionDB.SelectById(id);
            SubscriptionDB db = new SubscriptionDB();
            db.Delete(subscription);
            int x = db.SaveChanges();
            return x;
        }
        [HttpPut]
        [ActionName("UpdateASubscription")]
        public void UpdateASubscription([FromBody] Subscription subscription)
        {
            SubscriptionDB db = new SubscriptionDB();
            db.Update(subscription);
            int x = db.SaveChanges();
        }

        [HttpGet]
        [ActionName("TrainerSelector")]

        public TrainerList SelectAllTrainer()
        {
            TrainerDB db = new TrainerDB();
            TrainerList trainers = db.SelectAll();
            return trainers;
        }
        [HttpPost]
        [ActionName("InsertATrainer")]
        public int InsertATrainer(Trainer t)
        {
            TrainerDB db = new TrainerDB();
            db.Insert(t);
            int x = db.SaveChanges();
            return x;
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteATrainer")]

        public int DeleteATrainer(int id)
        {
            Trainer trainer = TrainerDB.SelectById(id);
            TrainerDB db = new TrainerDB();
            db.Delete(trainer);
            int x = db.SaveChanges();
            return x;
        }
        [HttpPut]
        [ActionName("UpdateATrainer")]
        public void UpdateATrainer([FromBody] Trainer trainer)
        {
            TrainerDB db = new TrainerDB();
            db.Update(trainer);
            int x = db.SaveChanges();
        }
        [HttpGet]
        [ActionName("TraineeSelector")]
        public TraineeList SelectAllTrainee()
        {
            TraineeDB db = new TraineeDB();
            TraineeList trainees = db.SelectAll();
            return trainees;
        }
        [HttpPost]
        [ActionName("InsertATrainee")]
        public int InsertATrainee(Trainee te)
        {
            TraineeDB db = new TraineeDB();
            db.Insert(te);
            int x = db.SaveChanges();
            return x;
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteATrainee")]
        public int DeleteATrainee(int id)
        {
            Trainee trainee = TraineeDB.SelectById(id);
            TraineeDB db = new TraineeDB();
            db.Delete(trainee);
            int x = db.SaveChanges();
            return x;
        }
        [HttpPut]
        [ActionName("UpdateATrainee")]
        public void UpdateATrainee([FromBody] Trainee trainee)
        {
            TraineeDB db = new TraineeDB();
            db.Update(trainee);
            int x = db.SaveChanges();
        }
        [HttpGet]
        [ActionName("Training_registrationSelector")]
        public Training_registrationList SelectAllTraining_registration()
        {
            Training_registrationDB db = new Training_registrationDB();
            Training_registrationList training_registrations = db.SelectAll();
            return training_registrations;
        }
        [HttpPost]
        [ActionName("InsertATraining_registration")]
        public int InsertATraining_registration(Training_registration tr)
        {
            Training_registrationDB db = new Training_registrationDB();
            db.Insert(tr);
            int x = db.SaveChanges();
            return x;
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteATraining_registration")]
        public int DeleteATraining_registration(int id)
        {
            Training_registration training_registration = Training_registrationDB.SelectById(id);
            Training_registrationDB db = new Training_registrationDB();
            db.Delete(training_registration);
            int x = db.SaveChanges();
            return x;
        }
        [HttpPut]
        [ActionName("UpdateATraining_registration")]
        public void UpdateATraining_registration([FromBody] Training_registration training_registration)
        {
            Training_registrationDB db = new Training_registrationDB();
            db.Update(training_registration);
            int x = db.SaveChanges();
        }
        [HttpGet]
        [ActionName("Workouts_of_trainersSelector")]

        public Workouts_of_trainersList SelectAllWorkouts_of_trainers()
        {
            Workouts_of_trainersDB db = new Workouts_of_trainersDB();
            Workouts_of_trainersList workouts_of_trainerss = db.SelectAll();
            return workouts_of_trainerss;
        }
        [HttpPost]
        [ActionName("InsertAWorkouts_of_trainers")]

        public int InsertAWorkouts_of_trainers(Workouts_of_trainers w)
        {
            Workouts_of_trainersDB db = new Workouts_of_trainersDB();
            db.Insert(w);
            int x = db.SaveChanges();
            return x;
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteAWorkouts_of_trainers")]

        public int DeleteAWorkouts_of_trainers(int id)
        {
            Workouts_of_trainers workouts_of_trainers = Workouts_of_trainersDB.SelectById(id);
            Workouts_of_trainersDB db = new Workouts_of_trainersDB();
            db.Delete(workouts_of_trainers);
            int x = db.SaveChanges();
            return x;
        }
        [HttpPut]
        [ActionName("UpdateAWorkouts_of_trainers")]
        public void UpdateAWorkouts_of_trainers([FromBody] Workouts_of_trainers workouts_of_trainers)
        {
            Workouts_of_trainersDB db = new Workouts_of_trainersDB();
            db.Update(workouts_of_trainers);
            int x = db.SaveChanges();
        }
        [HttpGet]
        [ActionName("Kinds_of_workoutsSelector")]

        public Kinds_of_workoutsList SelectAllKinds_of_workouts()
        {
            Kinds_of_workoutsDB db = new Kinds_of_workoutsDB();
            Kinds_of_workoutsList kinds_of_workoutss = db.SelectAll();
            return kinds_of_workoutss;
        }
        [HttpPost]
        [ActionName("InsertAKinds_of_workouts")]
        public int InsertAKinds_of_workouts(Kinds_of_workouts kw)
        {
            Kinds_of_workoutsDB db = new Kinds_of_workoutsDB();
            db.Insert(kw);
            int x = db.SaveChanges();
            return x;
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteAKinds_of_workouts")]
        public int DeleteAKinds_of_workouts(int id)
        {
            Kinds_of_workouts kinds_of_workouts = Kinds_of_workoutsDB.SelectById(id);
            Kinds_of_workoutsDB db = new Kinds_of_workoutsDB();
            db.Delete(kinds_of_workouts);
            int x = db.SaveChanges();
            return x;

        }
        [HttpPut]
        [ActionName("UpdateAKinds_of_workouts")]
        public void UpdateAKinds_of_workouts([FromBody] Kinds_of_workouts kinds_of_workouts)
        {
            Kinds_of_workoutsDB db = new Kinds_of_workoutsDB();
            db.Update(kinds_of_workouts);
            int x = db.SaveChanges();
        }
       

        [HttpGet]
        [ActionName("List_of_Exc_workoutsSelector")]

        public List_of_Exc_workoutsList SelectAllList_of_Exc_workouts()
        {
            List_of_Exc_workoutsDB db = new List_of_Exc_workoutsDB();
            List_of_Exc_workoutsList list_of_Exc_workoutss = db.SelectAll();
            return list_of_Exc_workoutss;
        }
        [HttpPost]
        [ActionName("InsertAList_of_Exc_workouts")]
        public int InsertAList_of_Exc_workouts(List_of_Exc_workouts l)
        {
            List_of_Exc_workoutsDB db = new List_of_Exc_workoutsDB();
            db.Insert(l);
            int x = db.SaveChanges();
            return x;
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteAList_of_Exc_workouts")]
        public int DeleteAList_of_Exc_workouts(int id)
        {
            List_of_Exc_workouts list_of_Exc_workouts = List_of_Exc_workoutsDB.SelectById(id);
            List_of_Exc_workoutsDB db = new List_of_Exc_workoutsDB();
            db.Delete(list_of_Exc_workouts);
            int x = db.SaveChanges();
            return x;

        }
        [HttpPut]
        [ActionName("UpdateAList_of_Exc_workouts")]
        public void UpdateAList_of_Exc_workouts([FromBody] List_of_Exc_workouts list_of_Exc_workouts)
        {
            List_of_Exc_workoutsDB db = new List_of_Exc_workoutsDB();
            db.Update(list_of_Exc_workouts);
            int x = db.SaveChanges();
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
