# Checkout Simulation: Queue vs Priority Queue

Simulates one checkout lane and compares serving customers in arrival order with serving the smallest basket first.

## How it works

- Ten customers wait with 7, 9, 5, 4, 15, 21, 6, 13, 3 and 2 items.
- Scanning one item takes 2.7 seconds. Payment time is ignored.
- The program prints the time at which each customer finishes and the average finishing time, once with a normal FIFO `Queue<int>` and once with a priority queue.
- The priority queue (`PQ`) keeps a `List<int>` sorted in ascending order at all times, so the customer with the fewest items is served first.

## Data structures

- `Queue<int>`
- `PQ`: priority queue built on a sorted `List<int>`

## Run

Open the `.sln` file in this folder with Visual Studio and press Ctrl+F5.

## Output

```
For Normal Queue:
1.customer's waiting time: 18.900000000000002s
2.customer's waiting time: 43.2s
3.customer's waiting time: 56.7s
4.customer's waiting time: 67.5s
5.customer's waiting time: 108s
6.customer's waiting time: 164.7s
7.customer's waiting time: 180.89999999999998s
8.customer's waiting time: 215.99999999999997s
9.customer's waiting time: 224.09999999999997s
10.customer's waiting time: 229.49999999999997s

Average waiting time per customer: 130.95s

For Priority Queue:
1.customer's waiting time: 5.4s
2.customer's waiting time: 13.500000000000002s
3.customer's waiting time: 24.300000000000004s
4.customer's waiting time: 37.800000000000004s
5.customer's waiting time: 54.00000000000001s
6.customer's waiting time: 72.9s
7.customer's waiting time: 97.2s
8.customer's waiting time: 132.3s
9.customer's waiting time: 172.8s
10.customer's waiting time: 229.5s

Average waiting time per customer: 83.97s
```

The small rounding noise comes from adding `double` values.
