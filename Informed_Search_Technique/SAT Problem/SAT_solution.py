from ortools.sat.python import cp_model

def solve_seating_problem():
    model = cp_model.CpModel()

    num_guests = 100
    num_tables = 10

    x = [[model.NewBoolVar(f'x_{i}_{j}') for j in range(num_tables)] for i in range(num_guests)]

    for i in range(num_guests):
        model.Add(sum(x[i][j] for j in range(num_tables)) == 1)

    for j in range(num_tables):
        model.Add(sum(x[i][j] for i in range(num_guests)) == 10)

    cannot_sit_together_pairs = [(1, 5), (2, 7), (10, 15)]  
    for pair in cannot_sit_together_pairs:
        G1, G5 = pair
        for j in range(num_tables):
            model.AddBoolOr([x[G1-1][j].Not(), x[G5-1][j].Not()])



    must_sit_together_pairs = [(3, 6), (8, 12), (20, 25)]  
    for pair in must_sit_together_pairs:
        G3, G6 = pair
        for j in range(num_tables):
            model.AddImplication(x[G3-1][j], x[G6-1][j])
            model.AddImplication(x[G6-1][j], x[G3-1][j])


    for i in range(num_guests - 1):
        for j in range(num_tables):
            for k in range(j + 1, num_tables):
                model.AddBoolOr([x[i][j].Not(), x[i + 1][k].Not(), x[i][j], x[i + 1][k]]).OnlyEnforceIf([x[i][j], x[i + 1][k]])
                model.AddBoolOr([x[i][j].Not(), x[i + 1][k].Not(), x[i + 1][k], x[i][j]]).OnlyEnforceIf([x[i + 1][k], x[i][j]])


    solver = cp_model.CpSolver()
    status = solver.Solve(model)

    if status == cp_model.OPTIMAL:
        print("Solution found:")
        for i in range(num_guests):
            for j in range(num_tables):
                if solver.Value(x[i][j]) == 1:
                    print(f"Guest {i+1} seated at Table {j+1}")
    elif status == cp_model.INFEASIBLE:
        print("No solution exists.")
    else:
        print("Solver did not find an optimal solution.")


solve_seating_problem()