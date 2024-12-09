namespace AoC.D09
{
    internal class Day09 : ISolver
    {
        private readonly string _inputFile;

        public Day09(string inputFile)
        {
            _inputFile = inputFile;
        }

        public async Task<string> SolvePart1()
        {
            int[] input = await ReadInput();
            int[] memory = GetMemory(input);
            CompactMemory(memory);

            long result = CalculateChecksum(memory);
            return result.ToString();
        }

        public async Task<string> SolvePart2()
        {
            int[] input = await ReadInput();
            int[] memory = GetMemory(input);

            List<(int fileID, int startAddress, int size)> files = GetFilesWithSizes(input);
            files.Reverse(); // process last file first

            List<(int size, int startAddress)> freeSpace = GetFreeSpace(input);
            IOrderedEnumerable<(int size, int startAddress)> orderedFreeSpace = freeSpace.OrderBy(x => x.startAddress);
            LinkedList<(int size, int startAddress)> freeSpaceList = new LinkedList<(int size, int startAddress)>(orderedFreeSpace);

            CompactMemoryV2(memory, files, freeSpaceList);

            long result = CalculateChecksum(memory);
            return result.ToString();
        }

        private int[] GetMemory(int[] input)
        {
            int n = input.Sum();
            int[] result = new int[n];

            bool isFile = true;
            int fileID = 0;
            int i = 0;
            foreach (int size in input)
            {
                int value = -1;
                if (isFile)
                {
                    value = fileID;
                    isFile = false;
                }
                else
                {
                    value = -1;
                    isFile = true;
                    fileID++;
                }

                for (int j = 0; j < size; j++)
                {
                    result[i++] = value;
                }
            }

            return result;
        }

        private List<(int fileID, int startAddress, int size)> GetFilesWithSizes(int[] input)
        {
            List<(int fileID, int size, int startIndex)> files = new();

            bool isFile = true;
            int fileID = 0;
            int i = 0;
            foreach (int size in input)
            {
                if (isFile)
                {
                    files.Add((fileID, i, size));
                    isFile = false;
                }
                else
                {
                    isFile = true;
                    fileID++;
                }

                i += size;
            }

            return files;
        }

        private List<(int size, int startAddress)> GetFreeSpace(int[] input)
        {
            List<(int size, int startAddress)> freeSpace = new();

            bool isFile = true;
            int i = 0;
            foreach (int size in input)
            {
                if (isFile)
                {
                    isFile = false;
                }
                else
                {
                    if (size > 0)
                    {
                        freeSpace.Add((size, i));
                    }
                    isFile = true;
                }

                i += size;
            }

            return freeSpace;
        }

        private void CompactMemory(int[] memory)
        {
            int i = 0;
            int j = memory.Length - 1;
            while (i < j)
            {
                // find memory cells to update
                while (j >= 0 && memory[j] == -1)
                {
                    j--;
                }

                while (i < memory.Length && memory[i] != -1)
                {
                    i++;
                }

                if (j < 0 || i >= memory.Length)
                {
                    return;
                }

                // compact
                memory[i] = memory[j];
                memory[j] = -1;

                // move to next memory cell
                i++;
                j--;
            }
        }

        private void CompactMemoryV2(int[] memory, List<(int fileID, int startAddress, int size)> files, LinkedList<(int size, int startAddress)> freeSpaceList)
        {
            foreach ((int fileID, int fileStartAddress, int fileSize) in files)
            {
                LinkedListNode<(int size, int startAddress)>? memoryNode = freeSpaceList.First;
                if (memoryNode == null)
                {
                    return;
                }

                // find the first memory node that fits the file
                while (memoryNode != null && memoryNode.Value.size < fileSize)
                {
                    memoryNode = memoryNode.Next;
                }

                // file is too large
                if (memoryNode == null)
                {
                    continue;
                }

                // free memory is on the right of the file
                if (memoryNode.Value.startAddress > fileStartAddress)
                {
                    continue;
                }

                WriteMemory(memory, fileStartAddress, fileSize, -1);
                WriteMemory(memory, memoryNode.Value.startAddress, fileSize, fileID);

                int remainingMemory = memoryNode.Value.size - fileSize;
                int newStartAddress = memoryNode.Value.startAddress + fileSize;

                var nextNode = memoryNode.Next;
                freeSpaceList.Remove(memoryNode);

                if (remainingMemory > 0)
                {
                    if (nextNode != null)
                    {
                        freeSpaceList.AddBefore(nextNode, (remainingMemory, newStartAddress));
                    }
                    else
                    {
                        freeSpaceList.AddLast((remainingMemory, newStartAddress));
                    }
                }
            }
        }

        private void WriteMemory(int[] memory, int startAddress, int size, int value)
        {
            for (int i = 0; i < size; i++)
            {
                memory[startAddress + i] = value;
            }
        }

        private long CalculateChecksum(int[] memory)
        {
            long result = 0;
            int i = 0;
            while (i < memory.Length)
            {
                if (memory[i] != -1)
                {
                    result += i * (long)memory[i];
                }

                i++;
            }
            return result;
        }

        private async Task<int[]> ReadInput()
        {
            string[] lines = await File.ReadAllLinesAsync(_inputFile);
            string line = lines[0];

            int[] result = new int[line.Length];
            for (int i = 0; i < line.Length; i++)
            {
                result[i] = line[i] - 48;
            }

            return result;
        }
    }
}