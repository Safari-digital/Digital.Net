using Digital.Net.Entities.Repositories;
using Digital.Net.Mvc.Controllers.Pagination;
using InternalTestProgram.Models;

namespace Digital.Net.Mvc.Test.TestUtilities.Controllers;

public class PaginationControllerWithId(IRepository<TestIdEntity> repository) : PaginationController<
    TestIdEntity, TestIdEntityDto, TestIdEntityQuery>(repository);