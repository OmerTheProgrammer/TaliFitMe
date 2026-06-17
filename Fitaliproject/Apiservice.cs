using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Model;
using System.Net.Http;
using System.Text.Json;

namespace Service
{
    public class Apiservice : IApiService
    {
        string uri;
        public HttpClient client;
        public Apiservice()
        {
            uri = "http://localhost:5278";
            client = new HttpClient();
            client.BaseAddress = new Uri(uri);
        }

        private static readonly System.Text.Json.JsonSerializerOptions JsonOptions =
        new System.Text.Json.JsonSerializerOptions
        {
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never
        };

        public async Task<string> GetPersonPhotoByte64(int id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"/api/Select/PersonPhotoSelector64Byte/{id}");

                if (response.IsSuccessStatusCode)
                {
                    // קריאת התשובה כטקסט פשוט וישיר מהשרת
                    string content = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(content))
                    {
                        // ניקוי מרכאות מיותרות אם השרת עטף את הטקסט
                        return content.Trim('"');
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[API Error]: {ex.Message}");
            }
            return null;
        }


        public async Task<PersonList>GetAllPerson()
        {
            return await client.GetFromJsonAsync<PersonList>("/api/Select/PersonSelector");
        }
        public async Task<int> DeleteAPerson(int id)
        {
            return  (await client.DeleteAsync("api/Select/DeleteAPerson/" + id)).IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> InsertAPerson(Person person)
        {
            return (await client.PostAsJsonAsync<Person>( "api/Select/InsertAPerson", person)).IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> UpdateAPerson(Person person)
        {
            return (await client.PutAsJsonAsync<Person>("api/Select/UpdateAPerson", person, JsonOptions)).IsSuccessStatusCode ? 1 : 0;
        }


        public async Task<ManagerList> GetAllManager()
        {
            return await client.GetFromJsonAsync<ManagerList>("/api/Select/ManagerSelector");
        }
        public async Task<int> DeleteAManager(int id)
        {
            return (await client.DeleteAsync("api/Select/DeleteAManager/" + id)).IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> InsertAManager(Manager manager)
        {
            return (await client.PostAsJsonAsync<Manager>("api/Select/InsertAManager", manager)).IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> UpdateAManager(Manager manager)
        {
            return (await client.PutAsJsonAsync<Manager>("api/Select/UpdateAManager", manager, JsonOptions)).IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<GenderList> GetAllGender()
        {
            return await client.GetFromJsonAsync<GenderList>("/api/Select/GenderSelector");
        }
        public async Task<int> DeleteAGender(int id)
        {
            return (await client.DeleteAsync("api/Select/DeleteAGender/" + id)).IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> InsertAGender(Gender gender)
        {
            return (await client.PostAsJsonAsync<Gender>("api/Select/InsertAGender", gender)).IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> UpdateAGender(Gender gender)
        {
            return (await client.PutAsJsonAsync<Gender>("api/Select/UpdateAGender", gender, JsonOptions)).IsSuccessStatusCode ? 1 : 0;
        }


        public async Task<SubscriptionList> GetAllSubscription()
        {
            return await client.GetFromJsonAsync<SubscriptionList>("/api/Select/SubscriptionSelector");
        }
        public async Task<int> DeleteASubscription(int id)
        {
            return (await client.DeleteAsync("api/Select/DeleteASubscription/" + id)).IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> InsertASubscription(Subscription subscription)
        {
            return (await client.PostAsJsonAsync<Subscription>("api/Select/InsertASubscription", subscription)).IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> UpdateASubscription(Subscription subscription)
        {
            return (await client.PutAsJsonAsync<Subscription>("api/Select/UpdateASubscription", subscription, JsonOptions)).IsSuccessStatusCode ? 1 : 0;
        }


        public async Task<TrainerList> GetAllTrainer()
        {
            return await client.GetFromJsonAsync<TrainerList>("/api/Select/TrainerSelector");
        }
        public async Task<int> DeleteATrainer(int id)
        {
            return (await client.DeleteAsync("api/Select/DeleteATrainer/" + id)).IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> InsertATrainer(Trainer trainer)
        {
            return (await client.PostAsJsonAsync<Trainer>("api/Select/InsertATrainer", trainer)).IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> UpdateATrainer(Trainer trainer)
        {
            var response = await client.PutAsJsonAsync<Trainer>("api/Select/UpdateATrainer", trainer, JsonOptions);
            return response.IsSuccessStatusCode ? 1 : 0;
        }



        public async Task<TraineeList> GetAllTrainee()
        {
            return await client.GetFromJsonAsync<TraineeList>("/api/Select/TraineeSelector");
        }
        public async Task<int> DeleteATrainee(int id)
        {
            return (await client.DeleteAsync("api/Select/DeleteATrainee/" + id)).IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> InsertATrainee(Trainee trainee)
        {
            return (await client.PostAsJsonAsync<Trainee>("api/Select/InsertATrainee", trainee)).IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> UpdateATrainee(Trainee trainee)
        {
            return (await client.PutAsJsonAsync<Trainee>("api/Select/UpdateATrainee", trainee, JsonOptions)).IsSuccessStatusCode ? 1 : 0;
        }


        public async Task<Kinds_of_workoutsList> GetAllKinds_of_workouts()
        {
            return await client.GetFromJsonAsync<Kinds_of_workoutsList>("/api/Select/Kinds_of_workoutsSelector");
        }
        public async Task<int> DeleteAKinds_of_workouts(int id)
        {
            return (await client.DeleteAsync("api/Select/DeleteAKinds_of_workouts/" + id)).IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> InsertAKinds_of_workouts(Kinds_of_workouts kinds_of_workouts)
        {
            return (await client.PostAsJsonAsync<Kinds_of_workouts>("api/Select/InsertAKinds_of_workouts", kinds_of_workouts)).IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> UpdateAKinds_of_workouts(Kinds_of_workouts kinds_of_workouts)
        {
            return (await client.PutAsJsonAsync<Kinds_of_workouts>("api/Select/UpdateAKinds_of_workouts", kinds_of_workouts, JsonOptions)).IsSuccessStatusCode ? 1 : 0;
        }



        public async Task<List_of_Exc_workoutsList> GetAllList_of_Exc_workouts()
        {
            return await client.GetFromJsonAsync<List_of_Exc_workoutsList>("/api/Select/List_of_Exc_workoutsSelector");
        }
        public async Task<int> DeleteAList_of_Exc_workouts(int id)
        {
            return (await client.DeleteAsync("api/Select/DeleteAList_of_Exc_workouts/" + id)).IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> InsertAList_of_Exc_workouts(List_of_Exc_workouts list_of_Exc_workouts)
        {
            return (await client.PostAsJsonAsync<List_of_Exc_workouts>("api/Select/InsertAList_of_Exc_workouts", list_of_Exc_workouts)).IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> UpdateAList_of_Exc_workouts(List_of_Exc_workouts list_of_Exc_workouts)
        {
            return (await client.PutAsJsonAsync<List_of_Exc_workouts>("api/Select/UpdateAList_of_Exc_workouts", list_of_Exc_workouts, JsonOptions)).IsSuccessStatusCode ? 1 : 0;
        }



        public async Task<Training_registrationList> GetAllTraining_registration()
        {
            return await client.GetFromJsonAsync<Training_registrationList>("/api/Select/Training_registrationSelector");
        }
        public async Task<int> DeleteATraining_registration(int id)
        {
            return (await client.DeleteAsync("api/Select/DeleteATraining_registration/" + id)).IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> InsertATraining_registration(Training_registration training_registration)
        {
            return (await client.PostAsJsonAsync<Training_registration>("api/Select/InsertATraining_registration", training_registration)).IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> UpdateATraining_registration(Training_registration training_registration)
        {
            return (await client.PutAsJsonAsync<Training_registration>("api/Select/UpdateATraining_registration", training_registration, JsonOptions)).IsSuccessStatusCode ? 1 : 0;
        }




        public async Task<Workouts_of_trainersList> GetAllWorkouts_of_trainers()
        {
            return await client.GetFromJsonAsync<Workouts_of_trainersList>("/api/Select/Workouts_of_trainersSelector");
        }
        public async Task<int> DeleteAWorkouts_of_trainers(int id)
        {
            return (await client.DeleteAsync("api/Select/DeleteAWorkouts_of_trainers/" + id)).IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> InsertAWorkouts_of_trainers(Workouts_of_trainers workouts_of_trainers)
        {
            return (await client.PostAsJsonAsync<Workouts_of_trainers>("api/Select/InsertAWorkouts_of_trainers", workouts_of_trainers)).IsSuccessStatusCode ? 1 : 0;
        }
        public async Task<int> UpdateAWorkouts_of_trainers(Workouts_of_trainers workouts_of_trainers)
        {
            return (await client.PutAsJsonAsync<Workouts_of_trainers>("api/Select/UpdateAWorkouts_of_trainers", workouts_of_trainers, JsonOptions)).IsSuccessStatusCode ? 1 : 0;
        }
    }
}
