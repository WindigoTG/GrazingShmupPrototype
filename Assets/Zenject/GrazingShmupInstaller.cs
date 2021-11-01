using UnityEngine;
using Zenject;
using GrazingShmup;

[CreateAssetMenu(fileName = "GrazingShmupInstaller", menuName = "Installers/GrazingShmupInstaller")]
public class GrazingShmupInstaller : ScriptableObjectInstaller<GrazingShmupInstaller>
{
    IPlayerFactory _playerFactory = new PlayerFactory();
    IEnemyFactory _enemyFactory = new EnemyFactory();
    IBulletFactory _bulletFactory = new BulletFactory();

    public override void InstallBindings()
    {
        Container.Bind<PlayerController>().FromNew().AsCached();
        Container.Bind<EnemyController>().FromNew().AsCached();
        Container.Bind<BulletManager>().FromNew().AsCached();
        Container.Bind<IPlayerFactory>().FromInstance(_playerFactory);
        Container.Bind<IEnemyFactory>().FromInstance(_enemyFactory);
        Container.Bind<IBulletFactory>().FromInstance(_bulletFactory);
        Container.Bind<CollisionManager>().FromNew().AsCached();
        Container.Bind<ObjectPoolManager>().FromNew().AsCached();
        Container.Bind<PlayerTracker>().FromNew().AsCached();
    }
}