using System;

namespace MasterWorkerEj;

public class Master
{
    private short[] v1;
    private short[] v2;
    private int numeroHilos;

    public Master(short[] v1, short[] v2, int numeroHilos)
    {
         if (numeroHilos < 1 || numeroHilos > v1.Length)
            throw new ArgumentException("El número de hilos debe ser menor o igual al tamaño del vector");
        
        this.v1 = v1;
        this.v2 = v2;
        this.numeroHilos = numeroHilos;
    }

    public double EncontrarCadena()
    {
        Worker[] workers = new Worker[this.numeroHilos];
        int numElementosPorHilo = this.v1.Length / numeroHilos;
        for (int i = 0; i < this.numeroHilos; i++)
        {
            int indiceDesde = i * numElementosPorHilo;
            int indiceHasta = (i + 1) * numElementosPorHilo -1 + (this.v2.Length-1);

            if (i == this.numeroHilos - 1)
            {
                indiceHasta = this.v1.Length - 1;
            }

            workers[i] = new Worker(this.v1, this.v2, indiceDesde, indiceHasta);
        }

        Thread[] hilos = new Thread[workers.Length];
        for (int i = 0; i < workers.Length; i++)
        {
            hilos[i] = new Thread(workers[i].Buscar);
            hilos[i].Name = "Worker número: " + (i + 1);
            hilos[i].Priority = ThreadPriority.Normal; 
            hilos[i].Start();  
        }

         foreach (Thread thread in hilos)
            thread.Join();

        // Recolectamos de los trabajadores.
        long resultado = 0;
        foreach (Worker worker in workers)
            resultado += worker.Encontrados;
        return resultado;
        
    }
}
