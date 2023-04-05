using System;
using SharedCode.Model;
using SharedCode.Model.Api;
using SharedCode.Util;
using System.Collections.Generic;
using System.Threading.Tasks;
using SharedCode.Model.DB;

namespace SharedCode.Repository.Interfaces
{
    public interface IPokemonRepository
    {
        Task<Result<List<ResultItem>>> GetPokemonList();
        Result<List<PokemonLocal>> GetPokemonLocalList();
        Task<Result<List<ResultItem>>> GetPokemonTypesList();
        Task<Result<List<ResultItem>>> GetPokemonGenerationList();
        Task<Result<PokemonSpecie>> GetPokemonInfo(int pokeId);
        Task<Result<TypeResponse>> GetTypeInfo(int typeId);
        Task<Result<GenerationResponse>> GetGenerationInfo(int generationId);
        Task<Result<byte[]>> GetPokemonImage(int pokeId);
    }
}

