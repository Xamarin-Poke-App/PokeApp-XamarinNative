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
        Task<Result<List<ResultItem>>> GetPokemonTypesList();
        Task<Result<List<ResultItem>>> GetPokemonGenerationList();
        Task<Result<PokemonSpecie>> GetPokemonSpecieInfo(int pokeId);
        Task<Result<TypeResponse>> GetTypeInfo(int typeId);
        Task<Result<GenerationResponse>> GetGenerationInfo(int generationId);
        Task<Result<EvolutionChainResponse>> GetEvolutionChainByPokemonId(int id);
    }
}

