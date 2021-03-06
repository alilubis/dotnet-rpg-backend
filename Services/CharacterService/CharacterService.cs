using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>{
            new Character(),
            new Character {Id=1, Name="Sam"}
        };
        private readonly IMapper _mapper;
        public CharacterService(IMapper mapper)
        {
            //this.mapper = mapper;
            _mapper = mapper;

        }
        //public List<Character> AddCharacter(Character newCharacter)
        //public async Task<List<Character>> AddCharacter(Character newCharacter)
        //public async Task<ServiceResponse<List<Character>>> AddCharacter(Character newCharacter)
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            //throw new System.NotImplementedException();
            //ServiceResponse<List<Character>> serviceResponse = new ServiceResponse<List<Character>>();
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            //characters.Add(_mapper.Map<Character>(newCharacter));
            //add id
            Character character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) +1;
            
            characters.Add(character);
            serviceResponse.Data = (characters.Select(c=> _mapper.Map<GetCharacterDto>(c))).ToList();
            //return characters;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            //throw new NotImplementedException();
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try{
            Character character = characters.First(c=>c.Id == id);
            characters.Remove(character);

            serviceResponse.Data = (characters.Select(c=> _mapper.Map<GetCharacterDto>(c))).ToList();
            }catch(Exception ex){
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            //throw new System.NotImplementedException();
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            //serviceResponse.Data = characters;
            serviceResponse.Data = (characters.Select(c=> _mapper.Map<GetCharacterDto>(c))).ToList();
            //return characters;
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            //throw new System.NotImplementedException();
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(characters.FirstOrDefault(c => c.Id == id));
            //return characters.FirstOrDefault(c => c.Id == id);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            //throw new System.NotImplementedException();
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            try{
            Character character = characters.FirstOrDefault(c=>c.Id == updateCharacter.Id);
            character.Name = updateCharacter.Name;
            character.Class = updateCharacter.Class;
            character.Defense = updateCharacter.Defense;
            character.HitPoints = updateCharacter.HitPoints;
            character.Intelligence = updateCharacter.Intelligence;
            character.Strength = updateCharacter.Strength;

            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }catch(Exception ex){
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}