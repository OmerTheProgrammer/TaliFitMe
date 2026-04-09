using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using ViewModel;

namespace Service
{
    public interface IApiService
    {
        public Task<PersonList> GetAllPerson();
        public Task<int>InsertAPerson(Person person);
        public Task<int> UpdateAPerson(Person person);
        public Task<int> DeleteAPerson(int id);
        public Task<GenderList> GetAllGender();
        public Task<int> InsertAGender(Gender gender);
        public Task<int> UpdateAGender(Gender gender);
        public Task<int> DeleteAGender(int id);

        public Task<ManagerList> GetAllManager();
        public Task<int> InsertAManager(Manager manager);
        public Task<int> UpdateAManager(Manager manager);
        public Task<int> DeleteAManager(int id);

        public Task<SubscriptionList> GetAllSubscription();
        public Task<int> InsertASubscription(Subscription subscription);
        public Task<int> UpdateASubscription(Subscription subscription);
        public Task<int> DeleteASubscription(int id);

        public Task<Kinds_of_workoutsList> GetAllKinds_of_workouts();
        public Task<int> InsertAKinds_of_workouts(Kinds_of_workouts kinds_of_workouts);
        public Task<int> UpdateAKinds_of_workouts(Kinds_of_workouts kinds_of_workouts);
        public Task<int> DeleteAKinds_of_workouts(int id);


        public Task<List_of_Exc_workoutsList> GetAllList_of_Exc_workouts();
        public Task<int> InsertAList_of_Exc_workouts(List_of_Exc_workouts list_of_Exc_workouts);
        public Task<int> UpdateAList_of_Exc_workouts(List_of_Exc_workouts list_of_Exc_workouts);
        public Task<int> DeleteAList_of_Exc_workouts(int id);



        public Task<TrainerList> GetAllTrainer();
        public Task<int> InsertATrainer(Trainer trainer);
        public Task<int> UpdateATrainer(Trainer trainer);
        public Task<int> DeleteATrainer(int id);

        public Task<TraineeList> GetAllTrainee();
        public Task<int> InsertATrainee(Trainee trainee);
        public Task<int> UpdateATrainee(Trainee trainee);
        public Task<int> DeleteATrainee(int id);

        public Task<Training_registrationList> GetAllTraining_registration();
        public Task<int> InsertATraining_registration(Training_registration training_registration);
        public Task<int> UpdateATraining_registration(Training_registration training_registration);
        public Task<int> DeleteATraining_registration(int id);

        public Task<Workouts_of_trainersList> GetAllWorkouts_of_trainers();
        public Task<int> InsertAWorkouts_of_trainers(Workouts_of_trainers workouts_of_trainers);
        public Task<int> UpdateAWorkouts_of_trainers(Workouts_of_trainers workouts_of_trainers);
        public Task<int> DeleteAWorkouts_of_trainers(int id);


    }
}
