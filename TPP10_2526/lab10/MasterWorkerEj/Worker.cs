using System;

namespace MasterWorkerEj;

public class Worker
{
    private short[] _v1;
    private short[] _v2;
    private int _indiceDesde;
    private int _indiceHasta;

    private int _encontrados;

    internal int Encontrados { get { return _encontrados; } }


    public Worker(short[] v1, short[] v2, int indiceDesde, int indiceHasta)
    {
        this._v1 = v1;
        this._v2 = v2;
        this._indiceDesde = indiceDesde;
        this._indiceHasta = indiceHasta;
    }

    internal void Buscar()
    {
        _encontrados = 0;
        for (int i = _indiceDesde; i <= _indiceHasta-(_v2.Length-1); i++)
            if (Coincide(i))
                _encontrados++;
    }

    private bool Coincide(int pos)
    {
        for (int i = 0; i < this._v2.Length; i++)
        {
            if (this._v1[pos+i] != this._v2[i])
                return false;
        }
        return true;
    }
}
