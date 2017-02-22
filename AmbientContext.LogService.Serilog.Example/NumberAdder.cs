using System;

namespace AmbientContext.LogService.Serilog.Example
{
    public class NumberAdder
    {
        public AmbientLogService Logger = new AmbientLogService();

        public void Add(params int[] numbersToAdd)
        {
            int sum = 0;
            foreach (var number in numbersToAdd)
            {
                sum += number;
            }

            Logger.Information("The sum is {Sum}", sum);

            if (sum % 10 != 0)
            {
                Logger.Warning("The sum {Sum} is not divisible by 10", sum);
            }

            if (sum % 2 == 0)
            {
                throw new Exception("The sum {Sum} is even. Abort !");
            }
        }
    }
}