﻿using DapperMVC.Data.DataAccess;
using DapperMVC.Data.Models.Domain;

namespace DapperMVC.Data.Repository
{
    public class PersonRepository : IPersonRepository
    {
        public readonly ISqlDataAccess _db;

        public PersonRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> AddAsync(Person person)
        {
            try
            {
                await _db.SaveData("sp_create_person", new { person.Name, person.Email, person.Address });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Person person)
        {
            try
            {
                //here we need id to update the Person object  so we passed whole object person 
                await _db.SaveData("sp_update_person", person);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _db.SaveData("sp_delete_person",new {Id = id });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            IEnumerable<Person> result = await _db.GetData<Person, dynamic>
                ("sp_get_person", new { Id = id });
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            string query = "sp_get_people";
            return await _db.GetData<Person, dynamic>(query, new { });
        }



    }
}
