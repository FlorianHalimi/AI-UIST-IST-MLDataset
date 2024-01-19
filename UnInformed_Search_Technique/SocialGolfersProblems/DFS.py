import itertools

def is_valid_group(group, played_with):
    for golfer in group:
        for other in group:
            if golfer != other and other in played_with[golfer]:
                return False
    return True

def add_group_to_played_with(group, played_with):
    for golfer in group:
        for other in group:
            if golfer != other:
                played_with[golfer].add(other)

def dfs(schedule, round_index, golfers, group_size, rounds, played_with):
    if round_index == rounds:
        return True

    if len(schedule) <= round_index:
        schedule.append([])

    if len(schedule[round_index]) * group_size == len(golfers):
        return dfs(schedule, round_index + 1, golfers, group_size, rounds, played_with)

    for group in itertools.combinations(golfers, group_size):
        if is_valid_group(group, played_with):
            add_group_to_played_with(group, played_with)
            schedule[round_index].append(group)
            if dfs(schedule, round_index, golfers, group_size, rounds, played_with):
                return True
            schedule[round_index].pop()
            for golfer in group:
                for other in group:
                    if golfer != other:
                        played_with[golfer].remove(other)

    return False

def social_golfer_formatted_output_newline(golfers, group_size):
    rounds = 8
    played_with = {golfer: set() for golfer in golfers}
    schedule = []

    if dfs(schedule, 0, golfers, group_size, rounds, played_with):
        formatted_schedule = "\n".join(
            [f"Week {week}:\n" + ", ".join(["-".join(map(str, group)) for group in groups]) for week, groups in enumerate(schedule, 1)]
        )
        return formatted_schedule
    else:
        return "No valid arrangement found"

# Example usage
golfers = list(range(1, 33))
group_size = 4
print(social_golfer_formatted_output_newline(golfers, group_size))
